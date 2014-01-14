using System;
using TinyIoC;
using VI.News.Client.iOS.Base;
using Locator.Mobile.Presentation;
using Locator.Mobile.iOS.Base;

namespace Locator.Mobile.iOS
{
	public class Bootstrapper
	{
		private static bool _isInitialize;
		private static readonly object SyncObj = new object();

		public static void Initialize()
		{
			lock (SyncObj)
			{
				if (_isInitialize) return;

				Client.Bootstrapper.Initialize();

				// платформозависимая логика
				TinyIoCContainer container = TinyIoCContainer.Current;
				container.Register<INavigation, Navigations>("navigations").AsSingleton();
				//container.Register<INotifyRegistration, NotifyRegistration>().AsMultiInstance();

				//if (DeviceInfo.IPad)
				//{
				//	var defaultSettings = container.Resolve<DefaultSettings>();
				//	defaultSettings.NewsFontSize.Current = 20f;
				//}

				_isInitialize = true;
			}
		}
	}
}

