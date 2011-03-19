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
using Rosette.Fractals.Colorization;
using Color = Rosette.Data.Color;

namespace Rosette.Windows.Controls
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public Color SelectedColor { get { return ViewModel.CurrentColor; } set { /* TODO */ } }

        private ColorPickerViewModel ViewModel { get { return DataContext as ColorPickerViewModel; } }

        public ColorPicker()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(ColorPicker_Loaded);
        }

        void ColorPicker_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWindowDataContext = DataContext as MainWindowViewModel;
            this.DataContext = new ColorPickerViewModel(mainWindowDataContext);
        }

        private void PaletteCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.OnPaletteMouseDown(e.GetPosition(PaletteCanvas));
        }

        private void PaletteCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            ViewModel.OnPaletteMouseMove(e.GetPosition(PaletteCanvas));
        }

        private void PaletteCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.OnPaletteMouseUp(e.GetPosition(PaletteCanvas));
        }

        private void HueCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.OnHueMouseDown(e.GetPosition(HueCanvas).Y);
        }

        private void HueCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.OnHueMouseUp(e.GetPosition(HueCanvas).Y);
        }

        private void HueCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            ViewModel.OnHueMouseMove(e.GetPosition(HueCanvas).Y);
        }
    }
}
