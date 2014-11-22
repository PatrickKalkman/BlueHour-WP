namespace BlueHour.WP8.Utils
{
    using System.IO.IsolatedStorage;

    public class SettingsHelper
    {
        private readonly IsolatedStorageSettings settings = 
            IsolatedStorageSettings.ApplicationSettings;

        public T GetSetting<T>(string settingName, T defaultValue)
        {
            if (!settings.Contains(settingName))
            {
                settings.Add(settingName, defaultValue);
            }

            return (T)settings[settingName];
        }

        public void UpdateSetting<T>(string settingName, T value)
        {
            if (!settings.Contains(settingName))
            {
                settings.Add(settingName, value);
            }
            else
            {
                settings[settingName] = value;
            }

            settings.Save();
        }
    }
}
