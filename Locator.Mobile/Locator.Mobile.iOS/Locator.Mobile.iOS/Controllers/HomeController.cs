using System;
using MonoTouch.UIKit;
using Locator.Mobile.iOS.Controllers.Friends;
using TinyIoC;
using Locator.Mobile.Presentation;
using Locator.ServiceContract;
using Locator.Entity.Entities;

namespace Locator.Mobile.iOS
{

	public class HomeController : UITabBarController
	{
		private UIViewController friendsTab, locationsTab;

		public HomeController ()
		{
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
	}
}
