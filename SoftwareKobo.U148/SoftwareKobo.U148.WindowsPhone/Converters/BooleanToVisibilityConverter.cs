using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool IsReversed
        {
            get;
            set;
        }

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

            if (IsReversed)
            {
                bValue = !bValue;
            }

            return bValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool bValue = false;

            if (value is Visibility)
            {
                bValue = (Visibility)value == Visibility.Visible;
            }

            if (IsReversed)
            {
                bValue = !bValue;
            }

            return bValue;
        }
    }
}