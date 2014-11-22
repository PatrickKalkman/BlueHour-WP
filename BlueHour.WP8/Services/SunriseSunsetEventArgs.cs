using BlueHour.WP8.Model;

namespace BlueHour.WP8.Services
{
    using System;

    public class SunriseSunsetEventArgs : EventArgs
    {
        private readonly SunriseSunsetList list;

        public SunriseSunsetEventArgs(SunriseSunsetList list, bool isValid)
        {
            this.list = list;
            this.IsValid = isValid;
        }

        public bool IsValid { get; set; }

        public SunriseSunsetList SunriseSunsetList
        {
            get
            {
                return this.list;
            }
        }
    }
}