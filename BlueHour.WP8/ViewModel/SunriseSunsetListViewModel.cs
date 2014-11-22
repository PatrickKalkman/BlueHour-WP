using System.Windows.Media;

using BlueHour.WP8.Resources;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BlueHour.WP8.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;

    using Model;
    using Services;
    using Services.Interfaces;
    using Utils;

    public class SunriseSunsetListViewModel : ViewModelBase, INavigable 
    {
        private readonly IAstronomyService astronomyService;

        private readonly BlueHourSettingsManager blueHourSettingsManager;

        private RelayCommand showSettingsCommand;

        private RelayCommand refreshCommand;

        private RelayCommand aboutCommand;

        private RelayCommand controlLoadedCommand;

        private bool isProgressBarVisible;

        private Visibility showListBox;

        public SunriseSunsetListViewModel(IAstronomyService astronomyService, BlueHourSettingsManager blueHourSettingsManager)
        {
            this.AboutCommand = new RelayCommand(() => NavigationService.Navigate("/Views/About.xaml"));

            this.astronomyService = astronomyService;
            this.blueHourSettingsManager = blueHourSettingsManager;
            this.astronomyService.LocationUpdated += this.AstronomyService_LocationUpdated;
            this.astronomyService.Start();
            this.SunriseSunsetViewModels = new ObservableCollection<SunriseSunsetViewModel>();
            this.ShowSettingsCommand = new RelayCommand(() => NavigationService.Navigate("/Views/Settings.xaml"));
            this.RefreshCommand = new RelayCommand(StartCalculation);
            this.AboutCommand = new RelayCommand(() => NavigationService.Navigate("/Views/About.xaml"));
            this.ControlLoadedCommand = new RelayCommand(this.ShowMessage);
            }

        public bool IsProgressBarVisible
        {
            get
            {
                return isProgressBarVisible;
            }

            set
            {
                isProgressBarVisible = value;
                RaisePropertyChanged(() => IsProgressBarVisible);  
            }
        }

        public Visibility ShowListBox
        {
            get
            {
                return showListBox;
            }

            set
            {
                showListBox = value;
                RaisePropertyChanged(() => ShowListBox);
            }
        }

        public ObservableCollection<SunriseSunsetViewModel> SunriseSunsetViewModels
        {
            get;
            set;
        }

        public SunriseSunsetViewModel SelectedSunriseSunset { get; set; }

        public RelayCommand ShowSettingsCommand
        {
            get
            {
                return showSettingsCommand;
            }

            set
            {
                showSettingsCommand = value;
                RaisePropertyChanged(() => ShowSettingsCommand);
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                return refreshCommand;
            }

            set
            {
                refreshCommand = value;
                RaisePropertyChanged(() => RefreshCommand);
            }
        }

        public RelayCommand ControlLoadedCommand
        {
            get
            {
                return controlLoadedCommand;
            }

            set
            {
                controlLoadedCommand = value;
                RaisePropertyChanged(() => ControlLoadedCommand);
            }
        }

        public RelayCommand AboutCommand
        {
            get
            {
                return aboutCommand;
            }

            set
            {
                aboutCommand = value;
                RaisePropertyChanged(() => AboutCommand);
            }
        }

        public INavigationService NavigationService { get; set; }

        public ImageBrush BackgroundImage
        {
            get { return new BackgroundImageBrush().GetBackground(); }
        }

        private void StartCalculation()
        {
            IsProgressBarVisible = true;
            ShowListBox = Visibility.Collapsed;
            this.astronomyService.Start();
        }

        private void ShowMessage()
        {
            if (!this.blueHourSettingsManager.IsLocationServiceAllowed)
            {
                string message = this.blueHourSettingsManager.IsLocationServiceAllowed
                                     ? AppResources.AllowedToGetLocationData
                                     : AppResources.NotAllowedToGetLocationData;
                MessageBox.Show(message);
            }
        }

        private void AstronomyService_LocationUpdated(object sender, SunriseSunsetEventArgs e)
        {
            if (e.IsValid)
            {
                astronomyService.Stop();
                this.SunriseSunsetViewModels.Clear();

                foreach (SunriseSunset sunriseSunset in e.SunriseSunsetList)
                {
                    this.SunriseSunsetViewModels.Add(new SunriseSunsetViewModel(sunriseSunset));
                }

                IsProgressBarVisible = false;
                ShowListBox = Visibility.Visible;
            }
        }
    }
}