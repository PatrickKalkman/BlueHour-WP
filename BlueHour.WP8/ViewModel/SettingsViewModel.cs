using System.Windows.Media;
using BlueHour.WP8.Views;

namespace BlueHour.WP8.ViewModel
{
    using Services;
    using Utils;

    public class SettingsViewModel : PropertyChangedEvent
    {
        private readonly BlueHourSettingsManager blueHourSettingsManager;
        private readonly BackgroundImageBrush backgroundImageBrush;

        public SettingsViewModel(BlueHourSettingsManager blueHourSettingsManager, BackgroundImageBrush backgroundImageBrush)
        {
            this.blueHourSettingsManager = blueHourSettingsManager;
            this.backgroundImageBrush = backgroundImageBrush;
        }

        public bool LocationServiceAllowed
        {
            get
            {
                return this.blueHourSettingsManager.IsLocationServiceAllowed;
            }
            set
            {
                this.blueHourSettingsManager.IsLocationServiceAllowed = value;
                OnPropertyChanged("LocationServiceAllowed");
            }
        }

        public ImageBrush BackgroundImage
        {
            get
            {
                return backgroundImageBrush.GetBackground();
            }
        }
    }
}
