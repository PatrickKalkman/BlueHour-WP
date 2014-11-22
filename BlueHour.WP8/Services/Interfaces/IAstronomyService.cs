namespace BlueHour.WP8.Services.Interfaces
{
    using System;

    public interface IAstronomyService
    {
        event EventHandler<SunriseSunsetEventArgs> LocationUpdated;

        void Start();

        void Stop();
    }
}