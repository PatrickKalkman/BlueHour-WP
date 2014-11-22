using BlueHour.WP8.Resources;

namespace BlueHour.WP8.Localization
{
    public class LocalizedStrings
    {
        private readonly AppResources localizedResources = new AppResources();

        public AppResources LocalizedResources
        {
            get
            {
                return localizedResources;
            }
        }
    }
}
