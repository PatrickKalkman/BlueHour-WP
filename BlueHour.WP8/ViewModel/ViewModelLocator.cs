using BlueHour.WP8.Services;
using BlueHour.WP8.Services.Interfaces;
using BlueHour.WP8.Utils;

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace BlueHour.WP8.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<BackgroundImageBrush>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<SettingsHelper>();
            SimpleIoc.Default.Register<SunCalculator>();
            SimpleIoc.Default.Register<BlueHourSettingsManager>();
            SimpleIoc.Default.Register<ILocationService, LocationService>();
            SimpleIoc.Default.Register<IAstronomyService, AstronomyService>();
            SimpleIoc.Default.Register<SunriseSunsetListViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
        }

        public SunriseSunsetListViewModel SunriseSunsetListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SunriseSunsetListViewModel>();
            }
        }

        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public AboutViewModel AboutViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutViewModel>();
            }
        }
    }
}
