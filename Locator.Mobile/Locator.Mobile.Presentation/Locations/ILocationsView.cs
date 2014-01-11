using System;
using Locator.Entity.Entities;
using Locator.ServiceContract.Models;

namespace Locator.Mobile.Presentation
{
    public interface ILocationsView : IBaseView
    {
        LocationsModel Locations { get; set; }
        Location Location { get; set; }

        event Action Refresh;
        event Action<long> OpenLocation;

        void Update(LocationsModel model);
    }
}
