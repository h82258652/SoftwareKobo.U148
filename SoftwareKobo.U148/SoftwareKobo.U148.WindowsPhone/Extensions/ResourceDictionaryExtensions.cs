using Windows.UI.Xaml;

namespace SoftwareKobo.U148.Extensions
{
    public static class ResourceDictionaryExtensions
    {
        public static object FindValue(this ResourceDictionary resouce, string key)
        {
            if (resouce.ContainsKey(key))
            {
                return resouce[key];
            }
            else
            {
                foreach (var theme in resouce.ThemeDictionaries)
                {
                    if ((string)theme.Key == key)
                    {
                        return theme.Value;
                    }
                }
                foreach (var childResouce in resouce.MergedDictionaries)
                {
                    var value = FindValue(childResouce, key);
                    if (value != null)
                    {
                        return value;
                    }
                }
                return null;
            }
        }

        public static T FindValue<T>(this ResourceDictionary resouce, string key)
        {
            return (T)FindValue(resouce, key);
        }
    }
}