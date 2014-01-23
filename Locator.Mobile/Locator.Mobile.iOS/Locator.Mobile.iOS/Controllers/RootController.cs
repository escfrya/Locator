using System;
using MonoTouch.UIKit;
using Locator.Mobile.iOS.Controllers.Friends;
using TinyIoC;
using Locator.Mobile.Presentation;
using Locator.ServiceContract;
using Locator.Entity.Entities;

namespace Locator.Mobile.iOS
{
	public class RootController : UIViewController, IDispatcher
	{
		private readonly TinyIoCContainer container;
		private AppController appController;
		public RootController()
		{
			container = AppDelegate.Container.GetChildContainer();
			container.Register<IDispatcher> (this);
			appController = container.Resolve<AppController>();
		}

		public bool Initialize()
		{
			return appController.AppStart ();
		}

		public void RegisterDevice(string token)
		{
			appController.RegisterDevice (new DeviceDto
				{
					DeviceAppId = token,
					ClientVersion = new Version(1,0,0,0).ToString(),
					PlatformType = PlatformType.iOS
				});
		}

		#region IDispatcher implementation

		public void Invoke (Action action)
		{
			InvokeOnMainThread(() => action());
		}

		#endregion
	}

}

