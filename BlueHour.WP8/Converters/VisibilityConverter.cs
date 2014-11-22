namespace BlueHour.WP8.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = false;
            if (value is bool)
            {
                visible = (bool)value;
            }

            return visible ? Visibility.Visible : Visibility.Collapsed; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is Visibility) && (((Visibility)value) == Visibility.Visible);
        }
    }
}