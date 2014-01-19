using Locator.Entity.Entities;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Request;
using Locator.Mobile.BL.ServiceClient;

namespace Locator.Mobile.Presentation
{
    public class FriendsPresenter : BasePresenter
    {
        private readonly IFriendsView view;

        public FriendsPresenter(IFriendsView view, IServiceCommandFactory factory, IDispatcher dispatcher, 
                                INavigation navigation, ICacheHelper cacheHelper) 
            : base(view, factory, dispatcher, navigation, cacheHelper)
        {
            this.view = view;

            view.Refresh += ViewOnRefresh;
            view.SendLocation += ViewOnSendLocation;
        }

        private void ViewOnRefresh()
        {
			ExecuteRequest(Factory.GetFriends(), view.Update);
        }

        private void ViewOnSendLocation(long userId, double latitude, double longitude)
        {
            var request = new SendLocationRequest
                {
                    location = new Location
                        {
                            ToUserId = userId,
                            Latitude = latitude,
							Longitude = longitude,
							Message = "test",
							Description = "test desc"
                        }
                };
            ExecuteRequest(Factory.SendLocation(request), model => { });
        }
    }
}
