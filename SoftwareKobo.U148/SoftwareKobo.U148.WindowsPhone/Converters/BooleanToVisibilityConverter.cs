using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool bValue = false;
            if (value is bool)
            {
                bValue = (bool)value;
            }
            if (value is bool?)
            {
                bool? temp = (bool?)value;
                if (temp.HasValue)
                {
                    bValue = temp.Value;
                }
            }
            return bValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility)
            {
                return (Visibility)value == Visibility.Visible;
            }
            else
            {
                return false;
            }
        }
    }
}