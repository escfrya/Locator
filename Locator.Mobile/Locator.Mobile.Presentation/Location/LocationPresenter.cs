using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.ServiceClient;

namespace Locator.Mobile.Presentation
{
    public class LocationPresenter : BasePresenter
    {
        private readonly ILocationView view;

        public LocationPresenter(ILocationView view, IServiceCommandFactory factory, IDispatcher dispatcher, 
                                 INavigation navigation, ICacheHelper cacheHelper) 
            : base(view, factory, dispatcher, navigation, cacheHelper)
        {
            this.view = view;

            view.Refresh += ViewOnRefresh;
        }

        private void ViewOnRefresh(long locationId)
        {
            ExecuteRequest(Factory.GetLocation(locationId.ToString()), view.Update);
        }
    }
}
