using System.Windows;
using Locator.Mobile.WP.ViewModels;
using Microsoft.Phone.Controls;

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
    }
}