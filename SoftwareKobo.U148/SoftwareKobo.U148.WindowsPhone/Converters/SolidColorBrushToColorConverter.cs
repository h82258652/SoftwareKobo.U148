using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace SoftwareKobo.U148.Converters
{
    public class SolidColorBrushToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush brush = (SolidColorBrush)value;
            return brush.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}