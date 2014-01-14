using System;
using MonoTouch.UIKit;

namespace Locator.Mobile.iOS.Base
{
    public static class DeviceInfo
    {
        static DeviceInfo()
        {
            IPhone = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
            IPad = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
            IPhone5 = IPhone && UIScreen.MainScreen.Bounds.Height > 560f;
            IPhone4 = IPhone && !IPhone5;
            Retina = UIScreen.MainScreen.Scale > 1.0f;
            int version = Convert.ToInt16(UIDevice.CurrentDevice.SystemVersion.Split('.')[0]);
            IOS7 = version >= 7;
            IOS6 = version <= 6;
            ScreenWidth = (int) UIScreen.MainScreen.Bounds.Width;
        }

        public static bool IPhone { get; private set; }
        public static bool IPhone5 { get; private set; }
        public static bool IPhone4 { get; private set; }
        public static bool IPad { get; private set; }
        public static bool Retina { get; private set; }
        public static bool IOS7 { get; private set; }
        public static bool IOS6 { get; private set; }
        public static int ScreenWidth { get; private set; }
    }
}