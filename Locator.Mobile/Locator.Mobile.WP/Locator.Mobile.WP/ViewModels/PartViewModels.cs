using System;
using Locator.Entity.Entities;
using Locator.Mobile.WP.Helpers;
using Locator.ServiceContract.Models;

namespace Locator.Mobile.WP.ViewModels
{
    partial class RegistrationViewModel
    {
        partial void RegisterMethod(object o)
        {
            Register(long.Parse(o.ToString()));
        }

        public void Result(bool isAuthenticated)
        {
            
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
