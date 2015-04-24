using Newtonsoft.Json;
using SoftwareKobo.U148.Models;
using System.ComponentModel;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SoftwareKobo.U148.Datas
{
    public class User : INotifyPropertyChanged
    {
        private static IPropertySet _localSettings = ApplicationData.Current.LocalSettings.Values;

        public event PropertyChangedEventHandler PropertyChanged;

        public UserInfo Data
        {
            get
            {
                if (_localSettings.ContainsKey(nameof(Data)))
                {
                    string json = (string)_localSettings[nameof(Data)];
                    try
                    {
                        return JsonConvert.DeserializeObject<UserInfo>(json);
                    }
                    catch (JsonException)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            set
            {
                string json = JsonConvert.SerializeObject(value);
                if (_localSettings.ContainsKey(nameof(Data)))
                {
                    _localSettings[nameof(Data)] = json;
                }
                else
                {
                    _localSettings.Add(nameof(Data), json);
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsLogined)));
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Icon)));
                }
            }
        }

        public bool IsLogined
        {
            get
            {
                return Data != null;
            }
        }

        public string Icon
        {
            get
            {
                var data = Data;
                if (data != null)
                {
                    return data.Icon;
                }
                else
                {
                    return "/Assets/head.png";
                }
            }
        }
    }
}