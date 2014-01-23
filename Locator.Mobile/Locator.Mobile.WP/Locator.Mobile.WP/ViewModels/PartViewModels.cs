using System;
using System.Windows;
using System.Windows.Navigation;
using Locator.Entity.Entities;
using Locator.Mobile.WP.Helpers;
using Locator.ServiceContract.Models;
using Xamarin.Auth;

namespace Locator.Mobile.WP.ViewModels
{
    partial class RegistrationViewModel
    {
        partial void RegisterMethod(object o)
        {
            Register((Action<OAuth2Authenticator>) o);
        }

        public void Result(bool isAuthenticated)
        {
            if (!isAuthenticated)
                MessageBox.Show("Auth error");
        }
    }

    partial class LocationViewModel
    {
        public event Action LocationUpdated;
        partial void RefreshMethod(object o)
        {
            Refresh(LocationId);
        }

        public void Update(Location location)
        {
            Location = location;
            LocationUpdated();
        }
    }

    partial class FriendsViewModel
    {
        partial void RefreshMethod(object o)
        {
            Refresh();
        }

        async partial void SendLocationMethod(object o)
        {
            var position = await (new GeoHelper()).GetGeoPosition();
            SendLocation(Friend.ID, position.Coordinate.Latitude, position.Coordinate.Longitude);
        }

        public void Update(FriendsModel model)
        {
            Friends = model;
        }

    }
    partial class LocationsViewModel
    {
        partial void RefreshMethod(object o)
        {
            Refresh();
        }

        partial void OpenLocationMethod(object o)
        {
            OpenLocation(Location.ID);
        }

        public void Update(LocationsModel model)
        {
            Locations = model;
        }
    }
}
