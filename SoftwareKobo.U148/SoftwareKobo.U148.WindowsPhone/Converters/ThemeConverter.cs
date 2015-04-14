using SoftwareKobo.U148.Datas;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class ThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var theme = (ThemeMode)value;
            switch (theme)
            {
                case ThemeMode.Day:
                    return ElementTheme.Light;

                case ThemeMode.Night:
                    return ElementTheme.Dark;

                default:
                    return ElementTheme.Default;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var theme = (ElementTheme)value;
            if (theme == ElementTheme.Dark)
            {
                return ThemeMode.Night;
            }
            else
            {
                return ThemeMode.Day;
            }
        }
    }
}