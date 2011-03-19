using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rosette.Data;
using Rosette.Fractals;
using Rosette.Fractals.Mandelbrot;
using Rosette.Fractals.Colorization;

namespace Rosette.Windows
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get { return DataContext as MainWindowViewModel; } set { DataContext = value; } }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImageCoverCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.BoundingBoxProvider.MouseDown(e, sender as Canvas);
        }

        private void ImageCoverCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            ViewModel.BoundingBoxProvider.MouseMove(e, sender as Canvas);
            ViewModel.UpdateScreenCoordiatesIterations(e.GetPosition(sender as Canvas));
        }

        private void ImageCoverCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.BoundingBoxProvider.MouseUp(e, sender as Canvas);
        }

        private void ImageCoverCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.History.Back();
        }

        private void TopMenuBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            if (e.ClickCount > 1)
                WindowState = WindowState == System.Windows.WindowState.Maximized ? System.Windows.WindowState.Normal : System.Windows.WindowState.Maximized;
        }

        private void InnerColorBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount > 1)
            {
                var colorizer = ((FrameworkElement)sender).DataContext as ColorListColorizerViewModel;
                ViewModel.OpenColorPickerFor(colorizer, ColorizerContext.InnerColor, null);
            }
        }

        private void ColorListItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var colorizer = ColorListColors.DataContext as ColorListColorizerViewModel;
            var selectedIndex = ColorListColors.SelectedIndex;
            ViewModel.OpenColorPickerFor(colorizer, ColorizerContext.Colors, selectedIndex);
        }

        private void ColorListItem_RemoveClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var listViewItem = FindVisualParent<ListViewItem>(button);

            var itemIndex = ColorListColors.ItemContainerGenerator.IndexFromContainer(listViewItem);
            var colorizer = ColorListColors.DataContext as ColorListColorizerViewModel;
            colorizer.Colors.RemoveAt(itemIndex);
        }

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the queried item.</param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, a null reference is being returned.</returns>
        public static T FindVisualParent<T>(DependencyObject child)
          where T : DependencyObject
        {
            // get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // we’ve reached the end of the tree
            if (parentObject == null) return null;

            // check if the parent matches the type we’re looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                // use recursion to proceed with next level
                return FindVisualParent<T>(parentObject);
            }
        }
    }
}