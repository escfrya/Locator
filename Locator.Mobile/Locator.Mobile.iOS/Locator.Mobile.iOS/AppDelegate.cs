using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Locator.Mobile.Client;
using Locator.Mobile.Presentation;
using Locator.Mobile.iOS.Controllers.Friends;
using Locator.Mobile.iOS.Base;
using Locator.ServiceContract;
using Locator.Entity.Entities;

namespace Locator.Mobile.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private readonly NSString _alertKey = new NSString("alert");
		private readonly NSString _apsKey = new NSString("aps");
		private readonly NSString _badgeKey = new NSString("badge");
		private readonly NSString _objectIdKey = new NSString("ObjectId");
		private readonly NSString _soundKey = new NSString("sound");
		private readonly NSString _typeKey = new NSString("Type");

		public static TinyIoC.TinyIoCContainer Container {get;private set;}
		// class-level declarations
		UIWindow window;
		UINavigationController navigationController;
		RootController rootController;
		Navigations navigations;
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Bootstrapper.Initialize();

			Container = TinyIoC.TinyIoCContainer.Current;
			Bootstrapper.Initialize();
			navigationController = new UINavigationController ();
			navigations = new Navigations (navigationController);
			AppDelegate.Container.Register<INavigation> (navigations);

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			rootController = new RootController ();
			if (rootController.Initialize ()) {

				ProcessNotification (options);

				// clear Badge number
				UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes (UIRemoteNotificationType.Alert
				| UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound);
			}

			window.RootViewController = navigationController;
			window.MakeKeyAndVisible ();
			return true;
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			string token = deviceToken.Description.Replace("<", "").Replace(">", "").Replace(" ", "");
			rootController.RegisterDevice (token);
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			// TODO: process push
			//Root.NotifyRegistration.ProcessNotification(userInfo,
			//	UIApplication.SharedApplication.ApplicationState == UIApplicationState.Active);
		}

		public void ProcessNotification(NSDictionary options)
		{
			if (options == null) return;

			// распаковываем пуш уведомление
			if (options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
			{
				options = (options[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary) ??
				          new NSDictionary();
			}

			if (options.ContainsKey(_apsKey) && options.ContainsKey(_typeKey))
			{
				//string typeStr = options[_typeKey].ToString();
				//ApsContent aps = ParsePushAps(options);

				if (options.ContainsKey(_objectIdKey)) {
					var objectId = long.Parse(options.ObjectForKey(_objectIdKey).ToString());
					navigations.OpenLocation(objectId);
				}
			}
		}

		private ApsContent ParsePushAps(NSDictionary options)
		{
			var apsContent = new ApsContent();
			var aps = options.ObjectForKey(_apsKey) as NSDictionary;

			if (aps == null) return apsContent;

			if (aps.ContainsKey(_alertKey))
			{
				apsContent.Alert = aps[_alertKey].ToString();
			}

			if (aps.ContainsKey(_badgeKey))
			{
				string badgeStr = aps[_badgeKey].ToString();
				apsContent.Badge = int.Parse(badgeStr);
			}

			if (aps.ContainsKey(_soundKey))
			{
				apsContent.Sound = aps[_soundKey].ToString();
			}

			return apsContent;
		}

		private class ApsContent
		{
			public string Alert { get; set; }
			public int Badge { get; set; }
			public string Sound { get; set; }
		}
	}
}

