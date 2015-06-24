using Newtonsoft.Json;
using SoftwareKobo.U148.Extensions;
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

        public static User Instance
        {
            get
            {
                return (User)App.Current.Resources.FindValue("User");
            }
        }

        public UserInfo Data
        {
            get
            {
                if (_localSettings.ContainsKey("Data"))
                {
                    string json = (string)_localSettings["Data"];
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
                if (value != null)
                {
                    string json = JsonConvert.SerializeObject(value);
                    if (_localSettings.ContainsKey("Data"))
                    {
                        _localSettings["Data"] = json;
                    }
                    else
                    {
                        _localSettings.Add("Data", json);
                    }
                }
                else
                {
                    if (_localSettings.ContainsKey("Data"))
                    {
                        _localSettings.Remove("Data");
                    }
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Data"));
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLogined"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Icon"));
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