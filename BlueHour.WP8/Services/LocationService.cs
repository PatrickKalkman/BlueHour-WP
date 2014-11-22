namespace BlueHour.WP8.Services
{
    using System;
    using System.Device.Location;

    public class LocationService : ILocationService
    {
        private readonly BlueHourSettingsManager blueHourSettingsManager;

        private readonly GeoCoordinateWatcher geoCoordinateWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);

        private GeoCoordinate lastLocation;

        private bool isDisabled;

        public LocationService(BlueHourSettingsManager blueHourSettingsManager)
        {
            this.blueHourSettingsManager = blueHourSettingsManager;
            geoCoordinateWatcher.PositionChanged += this.GeoCoordinateWatcher_PositionChanged;
            geoCoordinateWatcher.StatusChanged += this.GeoCoordinateWatcher_StatusChanged;
        }

        public event EventHandler<LocationEventArgs> LocationUpdated = delegate { };

        public bool IsDisabled
        {
            get
            {
                return this.isDisabled;
            }
        }

        public void Start()
        {
            if (this.blueHourSettingsManager.IsLocationServiceAllowed)
            {
                geoCoordinateWatcher.Start();
            }
        }

        public void Stop()
        {
            if (this.blueHourSettingsManager.IsLocationServiceAllowed)
            {
                geoCoordinateWatcher.Stop();
            }
        }

        private void GeoCoordinateWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    isDisabled = true;
                    break;
                case GeoPositionStatus.Initializing:
                    isDisabled = false;
                    break;
                case GeoPositionStatus.NoData:
                    isDisabled = false;
                    break;
                case GeoPositionStatus.Ready:
                    isDisabled = false;
                    break;
            }
        }

        private void GeoCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            lastLocation = e.Position.Location;
            LocationUpdated(this, new LocationEventArgs(lastLocation));
        }
    }
}