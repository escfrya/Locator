using System;
using MonoTouch.Foundation;

namespace Locator.Mobile.iOS.Extentions
{
	public static class StringExtentions
	{
		public static NSString ToNSString(this string str)
		{
			return new NSString(str);
		}
	}
}

