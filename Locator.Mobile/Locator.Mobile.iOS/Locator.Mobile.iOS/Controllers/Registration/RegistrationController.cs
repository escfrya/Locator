using System;
using VI.News.Client.iOS.Base.Controller;
using Locator.Mobile.Presentation;
using XibFree;
using MonoTouch.UIKit;
using Xamarin.Auth;

namespace Locator.Mobile.iOS
{
	public class RegistrationController : BaseUILayoutViewController<RegistrationPresenter, IRegistrationView>, IRegistrationView
	{
		public RegistrationController ()
		{
		}
		private Button _button;
		public override void LoadView ()
		{
			base.LoadView ();
			Layout.SubViews = new View[] {
				_button = new Button("Facebook",() => { 
					Register(auth => {
						PresentViewController (auth.GetUI (), true, null);
					}); 
				})
			};
		}

		public event Action<Action<OAuth2Authenticator>> Register;

		public void Result (bool isAuthenticated)
		{
			DismissViewController (true, null);
			//var v = new UIAlertView ("facebook", "auth = " + isAuthenticated.ToString(), null, null);
			//v.Show ();
		}

		public string Login { get; set; }

		class Button : NativeView
		{
			public Button(string title, Action handler)
			{
				// Setup the button
				var button = new UIButton(UIButtonType.RoundedRect);
				button.SetTitle(title, UIControlState.Normal);
				View = button;

				// Attach an event handler and forward the event
				button.TouchUpInside += (sender, e) => handler();

				// Setup the layout parameters
				LayoutParameters = new LayoutParameters(AutoSize.FillParent, 200);
			}
		}
	}
}

