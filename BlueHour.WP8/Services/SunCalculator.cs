﻿// SunCalculator.cs - Calculator for calculating SunRise, SunSet and
// maximum solar radiation of a specific date and time.
//
// Patrick Kalkman  pkalkie@gmail.com
//
// (C) Patrick Kalkman http://www.semanticarchitecture.net
//

namespace BlueHour.WP8.Services
{
    using System;

    /// <summary>
    /// This class is responsible for calculating sun related parameters such as
    /// SunRise, SunSet and maximum solar radiation of a specific date and time.
    /// </summary>
    public class SunCalculator
    {
        private double longitude;
        private double latituteInRadians;

        public void Initialize(double longitudeToUse, double latitude)
        {
            this.longitude = longitudeToUse;
            this.latituteInRadians = ConvertDegreeToRadian(latitude);
        }

        public DateTime CalculateSunRise(DateTime dateTime)
        {
            TimeZoneInfo current = TimeZoneInfo.Local;
            int dayNumberOfDateTime = ExtractDayNumber(dateTime);
            double differenceSunAndLocalTime = this.CalculateDifferenceSunAndLocalTime(dayNumberOfDateTime, current.IsDaylightSavingTime(dateTime));
            double declanationOfTheSun = this.CalculateDeclination(dayNumberOfDateTime);
            double tanSunPosition = this.CalculateTanSunPosition(declanationOfTheSun);
            int sunRiseInMinutes = CalculateSunRiseInternal(tanSunPosition, differenceSunAndLocalTime);
            return CreateDateTime(dateTime, sunRiseInMinutes);
        }

        public DateTime CalculateSunSet(DateTime dateTime)
        {
            TimeZoneInfo current = TimeZoneInfo.Local;
            int dayNumberOfDateTime = ExtractDayNumber(dateTime);
            double differenceSunAndLocalTime = this.CalculateDifferenceSunAndLocalTime(dayNumberOfDateTime, current.IsDaylightSavingTime(dateTime));
            double declanationOfTheSun = this.CalculateDeclination(dayNumberOfDateTime);
            double tanSunPosition = this.CalculateTanSunPosition(declanationOfTheSun);
            int sunSetInMinutes = CalculateSunSetInternal(tanSunPosition, differenceSunAndLocalTime);
            return CreateDateTime(dateTime, sunSetInMinutes);
        }

        public double CalculateMaximumSolarRadiation(DateTime dateTime)
        {
            TimeZoneInfo current = TimeZoneInfo.Local;
            int dayNumberOfDateTime = ExtractDayNumber(dateTime);
            double differenceSunAndLocalTime = this.CalculateDifferenceSunAndLocalTime(dayNumberOfDateTime, current.IsDaylightSavingTime(dateTime));
            int numberOfMinutesThisDay = GetNumberOfMinutesThisDay(dateTime, differenceSunAndLocalTime);
            double declanationOfTheSun = this.CalculateDeclination(dayNumberOfDateTime);
            double sinSunPosition = this.CalculateSinSunPosition(declanationOfTheSun);
            double cosSunPosition = this.CalculateCosSunPosition(declanationOfTheSun);
            double sinSunHeight = sinSunPosition + cosSunPosition * Math.Cos(2.0 * Math.PI * (numberOfMinutesThisDay + 720.0) / 1440.0) + 0.08;
            double sunConstantePart = Math.Cos(2.0 * Math.PI * dayNumberOfDateTime);
            double sunCorrection = 1370.0 * (1.0 + (0.033 * sunConstantePart));
            return CalculateMaximumSolarRadiationInternal(sinSunHeight, sunCorrection);
        }

        public double CalculateDeclination(int numberOfDaysSinceFirstOfJanuary)
        {
            return Math.Asin(-0.39795 * Math.Cos(2.0 * Math.PI * (numberOfDaysSinceFirstOfJanuary + 10.0) / 365.0));
        }

        private static int ExtractDayNumber(DateTime dateTime)
        {
            return dateTime.DayOfYear;
        }

        private static DateTime CreateDateTime(DateTime dateTime, int timeInMinutes)
        {
            int hour = timeInMinutes / 60;
            int minute = timeInMinutes - (hour * 60);
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, 00);
        }

        private static int CalculateSunRiseInternal(double tanSunPosition, double differenceSunAndLocalTime)
        {
            int sunRise = (int)(720.0 - 720.0 / Math.PI * Math.Acos(-tanSunPosition) - differenceSunAndLocalTime);
            sunRise = LimitSunRise(sunRise);
            return sunRise;
        }


        private static int CalculateSunSetInternal(double tanSunPosition, double differenceSunAndLocalTime)
        {
            int sunSet = (int)(720.0 + 720.0 / Math.PI * Math.Acos(-tanSunPosition) - differenceSunAndLocalTime);
            sunSet = LimitSunSet(sunSet);
            return sunSet;
        }

        private double CalculateTanSunPosition(double declanationOfTheSun)
        {
            double sinSunPosition = this.CalculateSinSunPosition(declanationOfTheSun);
            double cosSunPosition = this.CalculateCosSunPosition(declanationOfTheSun);
            double tanSunPosition = sinSunPosition / cosSunPosition;
            tanSunPosition = LimitTanSunPosition(tanSunPosition);
            return tanSunPosition;
        }

        private double CalculateCosSunPosition(double declanationOfTheSun)
        {
            return Math.Cos(this.latituteInRadians) * Math.Cos(declanationOfTheSun);
        }

        private double CalculateSinSunPosition(double declanationOfTheSun)
        {
            return Math.Sin(this.latituteInRadians) * Math.Sin(declanationOfTheSun);
        }

        private double CalculateDifferenceSunAndLocalTime(int dayNumberOfDateTime, bool useDaylightSaving)
        {
            double ellipticalOrbitPart1 = 7.95204 * Math.Sin((0.01768 * dayNumberOfDateTime) + 3.03217);
            double ellipticalOrbitPart2 = 9.98906 * Math.Sin((0.03383 * dayNumberOfDateTime) + 3.46870);

            double differenceSunAndLocalTime = ellipticalOrbitPart1 + ellipticalOrbitPart2 + (this.longitude - this.GetLongitudeTimeZone()) * 4;

            if (useDaylightSaving)
            {
                differenceSunAndLocalTime -= 60;
            }

            return differenceSunAndLocalTime;
        }

        private double GetLongitudeTimeZone()
        {
            int toAdd = 1;
            if (this.longitude < 0)
            {
                toAdd = -1;
            }
            int longitudeTimeZone = ((int)(this.longitude / 15) + toAdd) * 15;
            return longitudeTimeZone;
        }

        private static double LimitTanSunPosition(double tanSunPosition)
        {
            if (((int)tanSunPosition) < -1)
            {
                tanSunPosition = -1.0;
            }
            if (((int)tanSunPosition) > 1)
            {
                tanSunPosition = 1.0;
            }
            return tanSunPosition;
        }

        private static int LimitSunSet(int sunSet)
        {
            if (sunSet > 1439)
            {
                sunSet -= 1439;
            }
            return sunSet;
        }

        private static int LimitSunRise(int sunRise)
        {
            if (sunRise < 0)
            {
                sunRise += 1440;
            }
            return sunRise;
        }

        private static double ConvertDegreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }

        private static double CalculateMaximumSolarRadiationInternal(double sinSunHeight, double sunCorrection)
        {
            double maximumSolarRadiation;
            if ((sinSunHeight > 0.0) && Math.Abs(0.25 / sinSunHeight) < 50.0)
            {
                maximumSolarRadiation = sunCorrection * sinSunHeight * Math.Exp(-0.25 / sinSunHeight);
            }
            else
            {
                maximumSolarRadiation = 0;
            }
            return maximumSolarRadiation;
        }

        private static int GetNumberOfMinutesThisDay(DateTime dateTime, double differenceSunAndLocalTime)
        {
            return dateTime.Hour * 60 + dateTime.Minute + (int)differenceSunAndLocalTime;
        }
    }
}