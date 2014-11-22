namespace BlueHour.WP8.Services
{
    using System;

    public class NavigationService : INavigationService
    {
        private readonly System.Windows.Navigation.NavigationService navigationService;

        public NavigationService(System.Windows.Navigation.NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void Navigate(string url)
        {
            navigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
        }
    }
}
