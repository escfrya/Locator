using System;
using Locator.Mobile.Client.Droid;
using Android.Content;
using Locator.Mobile.Presentation;

namespace Locator.Mobile.Droid
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

				Locator.Mobile.Client.Bootstrapper.Initialize();

				// платформозависимая логика
				var container = TinyIoC.TinyIoCContainer.Current;

				//TODO: igor fix
				//var settings = container.Resolve<ISettings> ();
				//settings.ClientVersion = new Version (1, 0, 0, 0);
				//settings.Platform = PlatformType.Android;

				//container.Register<INavigation, Navigations> ("navigations").AsSingleton ();
				_isInitialize = true;
			}

		}
	}
}

