using System;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class MinimumConverter : IValueConverter
    {
        public object CompareTo
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IComparable)
            {
                var compareable = (IComparable)value;
                var result = compareable.CompareTo(System.Convert.ChangeType(CompareTo, value.GetType()));
                if (result <= 0)
                {
                    return value;
                }
                else
                {
                    return CompareTo + "+";
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}