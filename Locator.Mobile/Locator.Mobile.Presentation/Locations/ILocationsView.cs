using System;
using Locator.ServiceContract.Models;

namespace Locator.Mobile.Presentation
{
    public interface ILocationsView : IBaseView
    {
        LocationsModel Locations { get; set; }

        event Action Refresh;
        event Action<long> OpenLocation;

        void Update(LocationsModel model);
    }
}
