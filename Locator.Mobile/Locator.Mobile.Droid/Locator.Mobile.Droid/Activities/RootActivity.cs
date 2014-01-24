using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Locator.Mobile.Client.Droid;
using Locator.Mobile.Presentation;
using TinyIoC;

namespace Locator.Mobile.Droid
{
	[Activity (Label = "Locator", MainLauncher = true)]			
	public class RootActivity : BaseActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var appController = Container.Resolve<AppController >(new NamedParameterOverloads(new Dictionary<string, object> {
				{"dispatcher", null}
			}));
			//appController.LogOff ();
			appController.AppStart ();
		}
	}
}

