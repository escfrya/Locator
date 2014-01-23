using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.ServiceClient;
using Locator.Mobile.DAL;

namespace Locator.Mobile.Presentation
{
    public class LocationPresenter : BasePresenter
    {
        private readonly ILocationView view;

        public LocationPresenter(ILocationView view, IServiceCommandFactory factory, IDispatcher dispatcher,
                                 INavigation navigation, ICacheHelper cacheHelper, ISettingsRepository settings) 
            : base(view, factory, dispatcher, navigation, cacheHelper, settings)
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
