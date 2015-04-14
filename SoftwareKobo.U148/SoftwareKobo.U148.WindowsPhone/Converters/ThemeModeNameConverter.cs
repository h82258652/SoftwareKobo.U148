using SoftwareKobo.U148.Datas;
using System;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class ThemeModeNameConverter : IValueConverter
    {
        public bool IsReverseName
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ThemeMode theme = (ThemeMode)value;

            if (IsReverseName)
            {
                theme = 1 - theme;
            }

            switch (theme)
            {
                case ThemeMode.Day:
                    return "日间";

                case ThemeMode.Night:
                    return "夜间";

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}