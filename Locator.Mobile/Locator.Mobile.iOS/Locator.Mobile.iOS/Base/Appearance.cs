/*using MonoTouch.UIKit;
using Locator.Mobile.iOS.Extentions;

namespace Locator.Mobile.iOS.Base
{
    public static class Appearance
    {
        public static void Initialize()
        {
            UIBarButtonItem.Appearance.TintColor = UIColor.Black;

            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = UIColor.Black,
                Font = UIFontExtentions.Light(),
                TextShadowColor = UIColor.Clear,
                TextShadowOffset = new UIOffset(0, 0)
            });

            UILabel.Appearance.BackgroundColor = UIColor.Clear;

            UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                Font = UIFontExtentions.Light()
            }, UIControlState.Normal);


            if (DeviceInfo.IOS6)
            {
                UINavigationBar.Appearance.SetBackgroundImage(UIColorExtentions.Gray(248).ToImage(),
                    UIBarMetrics.Default);
                UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes
                {
                    Font = UIFontExtentions.Light(),
                    TextColor = UIColor.FromRGB(2, 121, 251),
                    TextShadowColor = UIColor.Clear,
                    TextShadowOffset = new UIOffset(0, 0)
                }, UIControlState.Normal);

                UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes
                {
                    Font = UIFontExtentions.Light(),
                    TextColor = UIColor.FromRGB(198, 222, 249),
                    TextShadowColor = UIColor.Clear,
                    TextShadowOffset = new UIOffset(0, 0)
                }, UIControlState.Highlighted);

                UIImage img = UIColor.Clear.ToImage(40, 40).StretchableImage(0, 0);
                UIBarButtonItem.Appearance.SetBackgroundImage(img, UIControlState.Normal, UIBarMetrics.Default);
                UIBarButtonItem.Appearance.SetBackgroundImage(img, UIControlState.Highlighted, UIBarMetrics.Default);
                UIBarButtonItem.Appearance.SetBackButtonBackgroundImage(Assets.Back(),
                    UIControlState.Normal, UIBarMetrics.Default);
                UIBarButtonItem.Appearance.SetBackButtonBackgroundImage(
                    Assets.BackHighlighted(), UIControlState.Highlighted, UIBarMetrics.Default);
                UIBarButtonItem.Appearance.SetBackButtonTitlePositionAdjustment(new UIOffset(2, 0), UIBarMetrics.Default);


                UISegmentedControl.Appearance.SetTitleTextAttributes(new UITextAttributes
                {
                    Font = UIFontExtentions.Light(13),
                    TextColor = UIColor.FromRGB(0, 122, 255),
                    TextShadowColor = UIColor.Clear,
                    TextShadowOffset = new UIOffset(0, 0)
                }, UIControlState.Normal);

                UISegmentedControl.Appearance.SetTitleTextAttributes(new UITextAttributes
                {
                    Font = UIFontExtentions.Light(13),
                    TextColor = UIColor.White,
                    TextShadowColor = UIColor.Clear,
                    TextShadowOffset = new UIOffset(0, 0)
                }, UIControlState.Highlighted);

                UISegmentedControl.Appearance.SetTitleTextAttributes(new UITextAttributes
                {
                    Font = UIFontExtentions.Light(13),
                    TextColor = UIColor.White,
                    TextShadowColor = UIColor.Clear,
                    TextShadowOffset = new UIOffset(0, 0)
                }, UIControlState.Selected);

                UISegmentedControl.Appearance.BackgroundColor = UIColor.Clear;
                UISegmentedControl.Appearance.SetBackgroundImage(UIColor.Clear.ToImage(1, 29), UIControlState.Normal,
                    UIBarMetrics.Default);
                UISegmentedControl.Appearance.SetBackgroundImage(UIColor.FromRGB(0, 122, 255).ToImage(1, 29),
                    UIControlState.Selected, UIBarMetrics.Default);
                UISegmentedControl.Appearance.SetBackgroundImage(UIColor.FromRGB(0, 122, 255).ToImage(1, 29),
                    UIControlState.Highlighted, UIBarMetrics.Default);

                UISegmentedControl.Appearance.SetDividerImage(UIColor.FromRGB(0, 122, 255).ToImage(1, 29),
                    UIControlState.Normal, UIControlState.Normal, UIBarMetrics.Default);
                UISegmentedControl.Appearance.TintColor = UIColor.Clear;
            }
        }

        public static void StyleButtons(this UINavigationItem navigation)
        {
        }
    }
}*/