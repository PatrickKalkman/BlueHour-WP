namespace BlueHour.WP8.Services
{
    using System;

    public interface ILocationService
    {
        event EventHandler<LocationEventArgs> LocationUpdated;

        void Start();

        void Stop();
    }
}