using System;
using Locator.Entity.Entities;
using Locator.ServiceContract.Models;

namespace Locator.Mobile.Presentation
{
    public interface IFriendsView : IBaseView
    {
        FriendsModel Friends { get; set; }
        User Friend { get; set; }

        event Action Refresh; 
        event Action<long, double, double> SendLocation;

        void Update(FriendsModel model);
    }
}
