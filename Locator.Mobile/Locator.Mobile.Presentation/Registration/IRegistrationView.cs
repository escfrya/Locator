﻿using System;
using Xamarin.Auth;

namespace Locator.Mobile.Presentation
{
    public interface IRegistrationView : IBaseView
    {
        string Login { get; set; }

		event Action<Action<OAuth2Authenticator>> Register;

        void Result(bool isAuthenticated);
    }
}
