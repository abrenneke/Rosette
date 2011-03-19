using System;
using System.Windows;
using System.Windows.Data;
using Rosette.Extensions;

namespace Rosette.Windows.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var boolValue = (bool)value;
            return boolValue ? Visibility.Visible : 
                (parameter != null && parameter is string && ((string)parameter).IEquals("collapse")) ? Visibility.Collapsed : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
