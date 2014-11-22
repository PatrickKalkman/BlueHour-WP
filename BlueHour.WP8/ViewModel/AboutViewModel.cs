using System.Windows.Media;
using BlueHour.WP8.Views;
using GalaSoft.MvvmLight.Command;

namespace BlueHour.WP8.ViewModel
{
    using System;
    using Utils;

    using Microsoft.Phone.Tasks;

    public class AboutViewModel : PropertyChangedEvent
    {
        private readonly BackgroundImageBrush backgroundImageBrush;
        private RelayCommand rateCommand;
        private RelayCommand shareCommand;
        private RelayCommand moreCommand;
        private RelayCommand contactCommand;

        public AboutViewModel(BackgroundImageBrush backgroundImageBrush)
        {
            this.backgroundImageBrush = backgroundImageBrush;
            this.RateCommand = new RelayCommand(RateApp);
            this.ShareCommand = new RelayCommand(ShareApp);
            this.MoreCommand = new RelayCommand(MoreApps);
            this.ContactCommand = new RelayCommand(ContactAuthor);
        }

        private void RateApp()
        {
            new MarketplaceReviewTask().Show();
        }

        private void ShareApp()
        {
            var shareTask = new ShareLinkTask();
            shareTask.Message = "#BlueHour an application to calculate the Blue hour period of your location.";
            shareTask.Title = "Blue Hour WP7 App";
            shareTask.LinkUri = new Uri("http://www.semanticarchitecture.net");
            shareTask.Show();
        }

        public void ContactAuthor()
        {
            var emailTask = new EmailComposeTask();
            emailTask.Subject = "#BlueHour support request";
            emailTask.To = "pkalkie@gmail.com";
            emailTask.Show();
        }

        public void MoreApps()
        {
            var searchTask = new MarketplaceSearchTask();
            searchTask.SearchTerms = "Patrick Kalkman";
            searchTask.Show();
        }

        public RelayCommand RateCommand
        {
            get
            {
                return rateCommand;
            }

            set
            {
                rateCommand = value;
                OnPropertyChanged("RateCommand");
            }
        }

        public RelayCommand ShareCommand
        {
            get
            {
                return shareCommand;
            }

            set
            {
                shareCommand = value;
                OnPropertyChanged("ShareCommand");
            }
        }

        public RelayCommand MoreCommand
        {
            get
            {
                return moreCommand;
            }

            set
            {
                moreCommand = value;
                OnPropertyChanged("MoreCommand");
            }
        }

        public RelayCommand ContactCommand
        {
            get
            {
                return contactCommand;
            }

            set
            {
                contactCommand = value;
                OnPropertyChanged("ContactCommand");
            }
        }

        public ImageBrush BackgroundImage
        {
            get { return backgroundImageBrush.GetBackground(); }
        }
    }
}
