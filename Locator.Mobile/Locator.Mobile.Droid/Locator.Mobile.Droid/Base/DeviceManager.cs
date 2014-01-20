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
using Android.Views.InputMethods;

namespace Locator.Mobile.Client.Droid
{
	public class DeviceManager
	{
		public static void ShowKeyboard(View view, Context context) 
		{
			view.RequestFocus();

			InputMethodManager inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
			inputMethodManager.ShowSoftInput(view, ShowFlags.Forced);
			inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
		}

		public static void HideKeyboard(View view, Context context) 
		{
			InputMethodManager inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
			inputMethodManager.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
		}
	}
}

