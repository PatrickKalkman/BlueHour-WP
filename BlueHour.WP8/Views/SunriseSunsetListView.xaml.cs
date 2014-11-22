using System;
using System.IO.IsolatedStorage;
using System.Windows;
using BlueHour.WP8.ViewModel;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace BlueHour.WP8.Views
{
    using Microsoft.Phone.Controls;

    public partial class SunriseSunsetListView : PhoneApplicationPage
    {
        public SunriseSunsetListView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var askforReview = (bool)IsolatedStorageSettings.ApplicationSettings["askforreview"];
            if (askforReview)
            {
                IsolatedStorageSettings.ApplicationSettings["askforreview"] = false;
                var returnvalue = MessageBox.Show("Thank you for using BlueHour, would you like to review this app?", "Please review my app", MessageBoxButton.OKCancel);
                if (returnvalue == MessageBoxResult.OK)
                {
                    var marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                }
            }
        }

        private void ApplicationBarSettingsButton_OnClick(object sender, EventArgs e)
        {
            var viewModel = this.DataContext as SunriseSunsetListViewModel;
            if (viewModel != null)
            {
                viewModel.ShowSettingsCommand.Execute(null);       
            }
        }

        private void ApplicationBarAboutButton_OnClick(object sender, EventArgs e)
        {
            var viewModel = this.DataContext as SunriseSunsetListViewModel;
            if (viewModel != null)
            {
                viewModel.AboutCommand.Execute(null);
            }
        }

        private void ApplicationBarRefreshButton_OnClick(object sender, EventArgs e)
        {
            var viewModel = this.DataContext as SunriseSunsetListViewModel;
            if (viewModel != null)
            {
                viewModel.RefreshCommand.Execute(null);
            }
        }
    }
}