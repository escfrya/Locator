using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.ServiceClient;
using Locator.Mobile.DAL;

namespace Locator.Mobile.Presentation
{
    public class LocationsPresenter : BasePresenter
    {
        private readonly ILocationsView view;

        public LocationsPresenter(ILocationsView view, IServiceCommandFactory factory, IDispatcher dispatcher,
                                  INavigation navigation, ICacheHelper cacheHelper, ISettingsRepository settings) 
            : base(view, factory, dispatcher, navigation, cacheHelper, settings)
        {
            this.view = view;

            view.Refresh += ViewOnRefresh;
            view.OpenLocation += ViewOnOpenLocation;
        }

        private void ViewOnOpenLocation(long locationId)
        {
            Navigation.OpenLocation(locationId);
        }

        private void ViewOnRefresh()
        {
            ExecuteRequest(Factory.GetLocations(), view.Update);
        }
    }
}
