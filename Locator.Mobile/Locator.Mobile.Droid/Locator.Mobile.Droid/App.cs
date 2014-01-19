using Android.App;
using Android.Content;
using System;
using Android.Runtime;


namespace Locator.Mobile.Droid
{
	[Application]
	public class App : Application
	{
		public static Context AppContext { get; private set; }

		public App(IntPtr handle, JniHandleOwnership transfer)
			: base(handle,transfer)
		{
			AppContext = ApplicationContext;
		}
		public override void OnCreate ()
		{
			base.OnCreate ();
		}
	}
}

