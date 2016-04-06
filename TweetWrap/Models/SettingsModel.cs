using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetWrap.Models
{
    public static class SettingsModel
    {
        private static Windows.Storage.ApplicationDataContainer localSettings;

        static Windows.Storage.ApplicationDataContainer LocalSettings
        {
            get
            {
                if (localSettings == null)
                {
                    localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                }
                return localSettings;
            }
        }

        public static void Set<T>(string key, T value)
        {
            LocalSettings.Values[key] = value;
        }

        public static T Get<T>(string key, T defaultValue)
        {
            if (LocalSettings.Values.ContainsKey(key))
            {
                return (T)LocalSettings.Values[key];
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
