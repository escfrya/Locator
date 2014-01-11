using System;
using Locator.ServiceContract.Models;

namespace Locator.Mobile.Presentation
{
    public interface IFriendsView : IBaseView
    {
        FriendsModel Friends { get; set; }

        event Action Refresh; 
        event Action<long> SendLocation;

        void Update(FriendsModel model);
    }
}
