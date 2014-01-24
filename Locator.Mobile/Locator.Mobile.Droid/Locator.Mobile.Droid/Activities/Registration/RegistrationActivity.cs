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
using Locator.Mobile.Presentation;
using Locator.Mobile.Client.Droid;
using Xamarin.Auth;

namespace Locator.Mobile.Droid
{
	[Activity (Label = "Locator")]			
	public class RegistrationActivity : BaseActivity<RegistrationPresenter, IRegistrationView>, IRegistrationView
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Registration);
			var button = FindViewById<Button> (Resource.Id.Login);
			button.Click += (sender, e) => {
				Register(auth => {
					StartActivity (auth.GetUI (this));
				});

			};
			// Create your application here
		}

		public event Action<Action<OAuth2Authenticator>> Register;

		public void Result (bool isAuthenticated)
		{

		}

		public string Login { get; set; }
	}
}

