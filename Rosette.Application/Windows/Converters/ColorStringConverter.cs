using System;
using System.Windows.Data;
using Rosette.Extensions;

namespace Rosette.Windows.Converters
{
    [ValueConversion(typeof(Rosette.Data.Color), typeof(string))]
    public class ColorStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is Rosette.Data.Color))
                return string.Empty;
            var color = (Rosette.Data.Color)value;
            return "R: {0}, G: {1}, B: {2}".With(color.R, color.G, color.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
