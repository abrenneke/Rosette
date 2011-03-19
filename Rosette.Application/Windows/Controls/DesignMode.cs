using System.Windows;
using System.Windows.Media;

namespace Rosette.Windows.Controls
{
    public class DesignMode
    {
        public static readonly DependencyProperty IsHiddenProperty = DependencyProperty.RegisterAttached("IsHidden", typeof(bool), typeof(DesignMode),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsHiddenChanged)));

        public static void SetIsHidden(UIElement element, bool value)
        {
            element.SetValue(IsHiddenProperty, value);
        }

        public static bool GetIsHidden(UIElement element)
        {
            return (bool)element.GetValue(IsHiddenProperty);
        }

        public static void OnIsHiddenChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(obj) && (bool)e.NewValue == true)
            {
                var element = (FrameworkElement)obj;
                element.Width = 0;
                element.Height = 0;
            }
        }
    }
}
