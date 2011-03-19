using System.Windows;
using System.Windows.Controls;

namespace Rosette.Windows.Controls
{
    public class RoundedButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(RoundedButton), new UIPropertyMetadata());
        public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }
    }
}
