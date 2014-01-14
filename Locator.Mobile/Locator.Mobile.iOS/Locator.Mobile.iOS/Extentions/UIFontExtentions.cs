using System;
using MonoTouch.UIKit;

namespace Locator.Mobile.iOS.Extentions
{
	public static class UIFontExtentions
	{
		private const string RegularFontName = "HelveticaNeue";
		private const string LightFontName = "HelveticaNeue-Light";
		private const string UltraLightFontName = "HelveticaNeue-UltraLight";
		private const string BoldFontName = "HelveticaNeue-Bold";
		private const string ItalicFontName = "HelveticaNeue-Oblique";

		public static UIFont Regular(float fontSize = 17f)
		{
			return UIFont.FromName(RegularFontName, fontSize);
		}

		public static UIFont Bold(float fontSize = 17f)
		{
			return UIFont.FromName(BoldFontName, fontSize);
		}

		public static UIFont Light(float fontSize = 17f)
		{
			return UIFont.FromName(LightFontName, fontSize);
		}

		public static UIFont UltraLight(float fontSize = 17f)
		{
			return UIFont.FromName(UltraLightFontName, fontSize);
		}

		public static UIFont Italic(float fontSize = 17f)
		{
			return UIFont.FromName(ItalicFontName, fontSize);
		}
	}
}

