using System;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Exceptions;
using Locator.Mobile.BL.ServiceClient;
using Locator.Mobile.DAL;
using Locator.Mobile.Presentation.Service;

namespace Locator.Mobile.Presentation
{
    public class BasePresenter
    {
        protected readonly IBaseView View;
        protected readonly IServiceCommandFactory Factory;
        protected readonly INavigation Navigation;
        protected readonly IDispatcher Dispatcher;
        protected readonly ICacheHelper CacheHelper;
        protected readonly ISettingsRepository Settings;

        public BasePresenter(IBaseView view, IServiceCommandFactory factory, IDispatcher dispatcher, 
            INavigation navigation, ICacheHelper cacheHelper, ISettingsRepository settings)
        {
            Factory = factory;
            Navigation = navigation;
            View = view;
            Dispatcher = dispatcher;
            CacheHelper = cacheHelper;
            Settings = settings;
        }

        protected void ExecuteRequest<TResponse>(BaseServiceCommand<TResponse> command, Action<TResponse> callback, string busyText = "Loading...")
            where TResponse : new()
        {
            if (View != null)
				View.ShowLockMessage(busyText);
            var serviceRequest = new ServiceRequest<TResponse>(Dispatcher, View, command, callback, CacheHelper);
            try
            {
                serviceRequest.Request();
            }
            catch (NotLoggedException ex)
            {
                Settings.Delete(RequestClient.AuthCookieName);
                Navigation.Registration();
            }
            
        }
    }
}
