using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doubanFMAudioPlayer
{
    public class Settings
    {
        public static IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public static void Put<T>(string key, T val)
        {
            if(settings.Contains(key))
            {
                settings[key] = val;
            }
            else
            {
                settings.Add(key, val);
            }
        }

        public static T Get<T>(string key)
        {
            if(settings.Contains(key))
            {
                return (T)settings[key];
            }
            throw new Exception("no such key found in settings");
        }
    }

}
