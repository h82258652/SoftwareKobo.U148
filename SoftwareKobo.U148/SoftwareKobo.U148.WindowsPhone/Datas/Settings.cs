using SoftwareKobo.U148.Extensions;
using System.ComponentModel;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SoftwareKobo.U148.Datas
{
    public class Settings : INotifyPropertyChanged
    {
        public static Settings Instance
        {
            get
            {
                return (Settings)App.Current.Resources.FindValue("Settings");
            }
        }

        public ThemeMode ThemeMode
        {
            get
            {
                if (_localSettings.ContainsKey(nameof(ThemeMode)))
                {
                    return (ThemeMode)_localSettings[nameof(ThemeMode)];
                }
                else
                {
                    return ThemeMode.Day;
                }
            }
            set
            {
                if (_localSettings.ContainsKey(nameof(ThemeMode)))
                {
                    _localSettings[nameof(ThemeMode)] = (int)value;
                }
                else
                {
                    _localSettings.Add(nameof(ThemeMode), (int)value);
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ThemeMode)));
                }
            }
        }

        private static IPropertySet _localSettings = ApplicationData.Current.LocalSettings.Values;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}