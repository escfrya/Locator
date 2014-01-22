using System;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.ServiceClient;
using Xamarin.Auth;

namespace Locator.Mobile.Presentation
{
    public class RegistrationPresenter : BasePresenter
    {
        private readonly IRegistrationView view;

        public RegistrationPresenter(IRegistrationView view, IServiceCommandFactory factory, 
            IDispatcher dispatcher, INavigation navigation, ICacheHelper cacheHelper) 
            : base(view, factory, dispatcher, navigation, cacheHelper)
        {
            this.view = view;
            view.Register += OnRegister;
        }

		private void OnRegister(Action<OAuth2Authenticator> callUI)
        {
            var auth = new OAuth2Authenticator(
                clientId: "1383755311885740",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
			auth.Completed += (sender, eventArgs) => {
				view.Result(eventArgs.IsAuthenticated);
				if (eventArgs.IsAuthenticated)
					Navigation.Friends();
			};
			callUI (auth);
        }
    }
}
