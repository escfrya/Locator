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
using Java.IO;
using Locator.Mobile.Presentation;
using Android;
using Locator.Mobile.Droid;

namespace Locator.Mobile.Client.Droid
{
	public static class BundleKeys
	{
		public const string ObjectKey = "ObjectId";
	}

	public class Navigations : INavigation
	{
		private Func<Context> _getContext;

		public Navigations(Func<Context> getContext)
		{
			_getContext = getContext;
		}

		#region INavigation implementation

		public void Friends ()
		{
			var context = _getContext ();
			var homeActivity = new Intent (context, typeof(FragmentTabs));
			context.StartActivity (homeActivity); 
		}

		public void Locations ()
		{
			throw new NotImplementedException ();
		}

		public void OpenLocation (long locationId)
		{
			var context = _getContext ();
			var locationActivity = new Intent (context, typeof(LocationActivity));
			locationActivity.PutExtra (BundleKeys.ObjectKey, locationId.ToString());
			context.StartActivity (locationActivity); 
		}

		public void Registration ()
		{
			var context = _getContext ();
			var regActivity = new Intent (context, typeof(RegistrationActivity));
			context.StartActivity (regActivity); 
		}

		public void GoBack ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

