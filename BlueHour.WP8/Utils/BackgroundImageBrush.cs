using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BlueHour.WP8.Utils
{
    public class BackgroundImageBrush
    {
        public ImageBrush GetBackground()
        {
            ImageBrush imageBrush = null;

            if ((Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible)
            { 
                imageBrush = new ImageBrush
                                 {
                                     ImageSource = new BitmapImage(new Uri("/Images/Background_Light.png", UriKind.Relative))
                                 };
            }
            else
            {
                imageBrush = new ImageBrush
                                 {
                                     ImageSource = new BitmapImage(new Uri("/Images/Background_Dark.png", UriKind.Relative))
                                 };
            }

            return imageBrush;
        }
    }
}