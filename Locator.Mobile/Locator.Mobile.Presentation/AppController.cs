using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Request;
using Locator.Mobile.BL.ServiceClient;
using Locator.Mobile.DAL;
using Locator.ServiceContract;

namespace Locator.Mobile.Presentation
{
    public class AppController : BasePresenter
    {
        public AppController(IServiceCommandFactory factory, IDispatcher dispatcher,
            INavigation navigation, ICacheHelper cacheHelper, ISettingsRepository settings)
            : base(null, factory, dispatcher, navigation, cacheHelper, settings)
        {
        }

		public void RegisterDevice(DeviceDto device)
        {
			ExecuteRequest(Factory.RegisterDevice(new RegisterDeviceRequest { device = device }), result => { });
        }

        public bool AppStart()
        {
            bool isAuth = false;
            if (Settings.Get(RequestClient.AuthCookieName) != null)
            {
                Navigation.Friends();
                isAuth = true;
            }
            else
            {
                Navigation.Registration();
            }
            return isAuth;
        }
    }
}
