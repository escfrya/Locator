using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Motivator.Mobile.Client;
using Motivator.Mobile.Presentation;

namespace Motivator.Mobile.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public static TinyIoC.TinyIoCContainer Container {get;private set;}
		// class-level declarations
		UIWindow window;
		UINavigationController navController;
		Motivator_Mobile_iOSViewController viewController;
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Container = Bootstrapper.Initialize();
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			viewController = new Motivator_Mobile_iOSViewController ();
			navController = new UINavigationController (viewController);

			AppDelegate.Container.Register<INavigation> (new Navigations (navController));

			window.RootViewController = navController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

