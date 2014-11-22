using BlueHour.WP8.Model;
using BlueHour.WP8.Services.Interfaces;

namespace BlueHour.WP8.Services
{
    using System;

    public class AstronomyService : IAstronomyService
    {
        private readonly SunCalculator sunCalculator;

        private readonly ILocationService locationService;

        public AstronomyService(SunCalculator sunCalculator, ILocationService locationService)
        {
            this.sunCalculator = sunCalculator;
            this.locationService = locationService;
            locationService.LocationUpdated += this.LocationService_LocationUpdated;
        }

        public event EventHandler<SunriseSunsetEventArgs> LocationUpdated = delegate { };

        public void Start()
        {
            locationService.Start();
        }

        public void Stop()
        {
            locationService.Stop();
        }
        
        private void LocationService_LocationUpdated(object sender, LocationEventArgs e)
        {
            sunCalculator.Initialize(e.LastLocation.Longitude, e.LastLocation.Latitude);

            DateTime calculateUntil = DateTime.Now.AddDays(30);
            var list = new SunriseSunsetList();
            for (DateTime iterator = DateTime.Now; iterator < calculateUntil; iterator = iterator.AddDays(1))
            {
                DateTime sunrise = sunCalculator.CalculateSunRise(iterator);
                DateTime sunset = sunCalculator.CalculateSunSet(iterator);
                list.Add(new SunriseSunset { Sunrise = sunrise, Sunset = sunset });
            }

            LocationUpdated(this, new SunriseSunsetEventArgs(list, true));
        }
    }
}