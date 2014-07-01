using System;
using Locator.Entity.Entities;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Request;
using Locator.Mobile.BL.ServiceClient;
using Locator.Mobile.DAL;
using Locator.ServiceContract.Models;
using Xamarin.Auth;

namespace Locator.Mobile.Presentation
{
    public class RegistrationPresenter : BasePresenter
    {
        private readonly IRegistrationView view;

        public RegistrationPresenter(IRegistrationView view, IServiceCommandFactory factory,
            IDispatcher dispatcher, INavigation navigation, ICacheHelper cacheHelper, ISettingsRepository settings) 
            : base(view, factory, dispatcher, navigation, cacheHelper, settings)
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
				{

				    var request = new RegistrationRequest
				        {
				            request = new RegistrationModel
				                {
                                    Token = eventArgs.Account.Properties["access_token"],
									//User = new User { Login = "egor@test.ru", Password = "test", DisplayName = "Egor" }
				                }
				        };
                    ExecuteRequest(Factory.Registration(request), res =>
                        {
                            if (res.Success)
                                Navigation.Friends();
                        });
				}
					
			};
			callUI (auth);
        }
    }
}
