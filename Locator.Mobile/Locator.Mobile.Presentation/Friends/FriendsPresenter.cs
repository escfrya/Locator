using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locator.Mobile.BL.Client;
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

        private void ViewOnSendLocation(long location)
        {
            
        }
    }
}
