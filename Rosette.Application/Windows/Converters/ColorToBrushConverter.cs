using System;
using System.Windows.Media;
using System.Windows.Data;

namespace Rosette.Windows.Converters
{
    [ValueConversion(typeof(Rosette.Data.Color), typeof(Brush))]
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is Rosette.Data.Color))
                return null;
            var brush = new SolidColorBrush((Rosette.Data.Color)value);
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
