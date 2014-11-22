namespace BlueHour.WP8.ViewModel
{
    using System;

    using Model;
    using Utils;

    public class SunriseSunsetViewModel : PropertyChangedEvent
    {
        private readonly SunriseSunset sunriseSunset;

        public SunriseSunsetViewModel(SunriseSunset sunriseSunset)
        {
            this.sunriseSunset = sunriseSunset;
        }

        public DateTime Sunrise
        {
            get
            {
                return sunriseSunset.Sunrise;
            }

            set
            {
                sunriseSunset.Sunrise = value;
                OnPropertyChanged("Sunrise");
            }
        }

        public DateTime Sunset 
        {
            get
            {
                return sunriseSunset.Sunset;
            }

            set
            {
                sunriseSunset.Sunset = value;
                OnPropertyChanged("Sunset");
            }
        }

        public string DateFormatted
        {
            get
            {
                return sunriseSunset.Sunrise.ToString(Constants.SunriseSunsetDateFormat);
            }
        }

        public string SunriseTimeFormatted
        {
            get
            {
                return sunriseSunset.Sunrise.ToString(Constants.SunriseSunsetTimeFormat);
            }
        }

        public string SunsetTimeFormatted
        {
            get
            {
                return sunriseSunset.Sunset.ToString(Constants.SunriseSunsetTimeFormat);
            }
        }
    }
}