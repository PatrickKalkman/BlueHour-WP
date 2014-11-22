namespace BlueHour.WP8.Services
{
    using System;
    using System.Device.Location;

    public class LocationEventArgs : EventArgs
    {
        private readonly Location lastLocation;

        public LocationEventArgs(GeoCoordinate currentLocation)
        {
            this.lastLocation = new Location();
            this.lastLocation.Latitude = currentLocation.Latitude;
            this.lastLocation.Longitude = currentLocation.Longitude;
        }

        public Location LastLocation 
        {
            get
            {
                return this.lastLocation;
            }
        }
    }
}