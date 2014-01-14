using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace Locator.Mobile.iOS.Extentions
{
	public static class UIColorExtentions
	{
		public static UIImage ToImage(this UIColor color, float width = 1, float height = 1)
		{
			UIGraphics.BeginImageContextWithOptions(new SizeF(width, height), false, 0);
			CGContext context = UIGraphics.GetCurrentContext();
			context.SetFillColorWithColor(color.CGColor);
			context.FillRect(new RectangleF(0, 0, width, height));
			return UIGraphics.GetImageFromCurrentImageContext();
		}

		public static UIView ToView(this UIColor color)
		{
			return new UIView {BackgroundColor = color};
		}

		public static UIColor Gray(byte color)
		{
			return UIColor.FromRGB(color, color, color);
		}
	}
}

