using System;
using System.Windows.Data;
using Rosette.Data;
using Rosette.Extensions;

namespace Rosette.Windows.Converters
{
    [ValueConversion(typeof(Dimensions), typeof(string))]
    public class DimensionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dimensions = (Dimensions)value;
            var property = (string)parameter;

            if (property.IEquals("width"))
                return dimensions.Width.ToString();
            if (property.IEquals("height"))
                return dimensions.Height.ToString();
            return dimensions.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
