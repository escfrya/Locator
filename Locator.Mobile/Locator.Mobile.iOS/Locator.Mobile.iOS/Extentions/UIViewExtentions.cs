using System;
using MonoTouch.UIKit;

namespace Locator.Mobile.iOS.Extentions
{
	public static class UIViewExtentions
	{
		public static T Setup<T>(this T view, Action<T> setup)
			where T : UIView
		{
			setup(view);
			return view;
		}
	}
}

