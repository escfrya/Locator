using System;
using Locator.Mobile.Presentation;

namespace Locator.Mobile.WP
{
    public class Navigation : INavigation
    {
        public void Friends()
        {
            App.RootFrame.Navigate(new Uri("/Pages/FriendsPage.xaml", UriKind.Relative));
        }

        public void Locations()
        {
            App.RootFrame.Navigate(new Uri("/Pages/LocationsPage.xaml", UriKind.Relative));
        }

        public void OpenLocation(long locationId)
        {
            App.RootFrame.Navigate(new Uri(string.Format("/Pages/LocationPage.xaml?ObjectId={0}", locationId), UriKind.Relative));
        }

        public void Registration()
        {
            App.RootFrame.Navigate(new Uri("/Pages/RegistrationPage.xaml", UriKind.Relative));
        }

        public void GoBack()
        {
            if (App.RootFrame.CanGoBack)
                App.RootFrame.GoBack();
        }
    }
}
