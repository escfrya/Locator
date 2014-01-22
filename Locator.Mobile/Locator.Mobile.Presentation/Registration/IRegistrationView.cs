using System;

namespace Locator.Mobile.Presentation
{
    public interface IRegistrationView : IBaseView
    {
        string Login { get; set; }

        event Action<long> Register;

        void Result(bool isAuthenticated);
    }
}
