using BlueHour.WP8.Utils;

namespace BlueHour.WP8.Services
{
    public class BlueHourSettingsManager
    {
        private readonly SettingsHelper settingsHelper;

        public BlueHourSettingsManager(SettingsHelper settingsHelper)
        {
            this.settingsHelper = settingsHelper;
        }

        public bool IsLocationServiceAllowed
        {
            get
            {
                return this.settingsHelper.GetSetting(Constants.Settings.AllowAccessToLocationServicesKey, false); 
            }

            set
            {
                this.settingsHelper.UpdateSetting(Constants.Settings.AllowAccessToLocationServicesKey, value);  
            }
        }
    }
}