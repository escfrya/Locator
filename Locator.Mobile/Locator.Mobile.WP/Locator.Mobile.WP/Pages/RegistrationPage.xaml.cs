using System;
using System.Windows;
using Locator.Mobile.WP.ViewModels;
using Microsoft.Phone.Controls;
using Xamarin.Auth;

namespace Locator.Mobile.WP.Pages
{
    public partial class RegistrationPage : PhoneApplicationPage
    {
        private readonly RegistrationViewModel viewModel;
        private bool firstLoad = true;

        public RegistrationPage()
        {
            InitializeComponent();
            viewModel = App.Container.Resolve<RegistrationViewModel>();
            DataContext = viewModel;
        }

        private void PhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            if (firstLoad)
            {
                viewModel.ViewLoad();
                firstLoad = false;
            }
        }

        private void FacebookOnClick(object sender, RoutedEventArgs e)
        {

            Action<OAuth2Authenticator> callback = auth =>
                {
                    App.RootFrame.Navigate(auth.GetUI());
                };
            viewModel.RegisterCommand.Execute(callback);
        }
    }
}