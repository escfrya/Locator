using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Locator.Mobile.Presentation;
using Locator.Mobile.iOS.Controllers.Friends;

namespace Locator.Mobile.iOS.Base
{
	public class Navigations : INavigation
	{
		private readonly UINavigationController controller;

		public Navigations()
		{
		}

		public Navigations (UINavigationController controller)
		{
			this.controller = controller;
		}

		#region INavigations implementation

		public void Friends ()
		{
			var friendsController = new HomeController ();
			controller.SetViewControllers (new UIViewController[]{ friendsController }, true);
			//controller.PushViewController (friendsController, true);
		}

		public void Locations ()
		{
			throw new NotImplementedException ();
		}

		public void OpenLocation (long locationId)
		{
			var locationController = new LocationController ();
			locationController.LocationId = locationId;
			controller.PushViewController (locationController, true);
		}

		public void Registration ()
		{
			var regController = new RegistrationController ();
			controller.SetViewControllers (new UIViewController[]{ regController }, true);
		}

		public void GoBack()
		{
			controller.PopViewControllerAnimated (true);
		}
		#endregion
	}
}

