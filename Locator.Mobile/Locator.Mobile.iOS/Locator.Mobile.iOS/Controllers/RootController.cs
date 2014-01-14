using System;
using MonoTouch.UIKit;
using Locator.Mobile.iOS.Controllers.Friends;
using TinyIoC;
using Locator.Mobile.Presentation;
using Locator.ServiceContract;
using Locator.Entity.Entities;

namespace Locator.Mobile.iOS
{
	public class RootController : UITabBarController, IDispatcher
	{
		private readonly TinyIoCContainer container;
		private AppController appController;
		private UIViewController friendsTab, locationsTab;

		public RootController ()
		{
			container = AppDelegate.Container.GetChildContainer();
			container.Register<IDispatcher> (this);
			appController = container.Resolve<AppController>();

			friendsTab = new FriendsController();
			friendsTab.Title = "Friends";

			locationsTab= new LocationsController();
			locationsTab.Title = "Locations";

			var tabs = new UIViewController[] 
			{
				friendsTab, locationsTab
			};

			ViewControllers = tabs;
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

