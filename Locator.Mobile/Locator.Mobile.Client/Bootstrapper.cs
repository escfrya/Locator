using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.ServiceClient;
using Locator.Mobile.Client.ClientImpl;
using Locator.Mobile.DAL;
using TinyIoC;

namespace Locator.Mobile.Client
{
    /// <summary>
    /// Регистрация приложения в IoC
    /// </summary>
    public static class Bootstrapper
    {
        private static bool isInitialize;
        private static readonly object syncObj = new object();

        public static TinyIoCContainer Initialize()
        {
            TinyIoCContainer container = TinyIoCContainer.Current;
            lock (syncObj)
            {
                if (!isInitialize)
                {
                    Register(container);
                    isInitialize = true;
                }
            }
            return container;
        }

        private static void Register(TinyIoCContainer container)
        {
            container.Register<IServiceCommandFactory, ServiceCommandFactory>().AsMultiInstance();
            container.Register<ISerializer, JsonNetSerializer>().AsSingleton();
            //container.Register<IRequestClient, RestSharpClient>().AsMultiInstance();
            container.Register<IRequestClient, RestWebClient>().AsMultiInstance();
            container.Register<IRequestsRepository, RequestsRepository>();
            //container.Register<IActionRepository, ActionRepository>();
            container.Register<ICacheHelper, CacheHelper>();
        }
    }
}
