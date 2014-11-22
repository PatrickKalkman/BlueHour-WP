namespace BlueHour.WP8.Utils
{
    using Microsoft.Phone.Shell;

    public static class ApplicationBarExtensions
    {
        public static void UpdateTextButton(this IApplicationBar appBar, int index, string text)
        {
            var button = appBar.Buttons[index] as ApplicationBarIconButton;
            if (null != button)
            {
                button.Text = text.ToLower();
            }
        }

        public static void UpdateTextMenuItem(this IApplicationBar appBar, int index, string text)
        {
            var menuItem = appBar.MenuItems[index] as ApplicationBarMenuItem;
            if (null != menuItem)
            {
                menuItem.Text = text;
            }
        }
    }
}