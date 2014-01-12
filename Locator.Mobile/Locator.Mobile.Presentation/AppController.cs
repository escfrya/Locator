using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Request;
using Locator.Mobile.BL.ServiceClient;

namespace Locator.Mobile.Presentation
{
    public class AppController : BasePresenter
    {
        public AppController(IServiceCommandFactory factory, IDispatcher dispatcher, INavigation navigation, ICacheHelper cacheHelper)
            : base(null, factory, dispatcher, navigation, cacheHelper)
        {
        }

        public void RegisterDevice(RegisterDeviceRequest request)
        {
            ExecuteRequest(Factory.RegisterDevice(request), result => { });
        }
    }
}
