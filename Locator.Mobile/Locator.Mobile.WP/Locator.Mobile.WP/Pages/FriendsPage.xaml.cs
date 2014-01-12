using System.Windows;
using System.Windows.Controls;
using Locator.Mobile.WP.ViewModels;
using Microsoft.Phone.Controls;

namespace Locator.Mobile.WP.Pages
{
    public class FLViewModel
    {
        public FriendsViewModel FVM { get; set; }
        public LocationsViewModel LVM { get; set; }
    }

    public partial class FriendsPage : PhoneApplicationPage
    {
        private readonly FLViewModel viewModel;
        private bool userClickToItem;
        private bool firstLoad = true;

        public FriendsPage()
        {
            InitializeComponent();
            viewModel = new FLViewModel
                {
                    FVM = App.Container.Resolve<FriendsViewModel>(),
                    LVM = App.Container.Resolve<LocationsViewModel>()
                };
            DataContext = viewModel;
        }

        private void OnFriendsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userClickToItem && viewModel.FVM.Friend != null)
                viewModel.FVM.SendLocationCommand.Execute(null);
        }

        private void OnLocationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userClickToItem && viewModel.LVM.Location != null)
                viewModel.LVM.OpenLocationCommand.Execute(null);
        }

        private void PhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            if (firstLoad)
            {
                viewModel.FVM.ViewLoad();
                viewModel.LVM.ViewLoad();
                firstLoad = false;
            }
            viewModel.FVM.ParseParams(NavigationContext.QueryString);
            viewModel.FVM.RefreshCommand.Execute(null);
            viewModel.LVM.RefreshCommand.Execute(null);
            userClickToItem = true;
        }

        private void PhoneApplicationPageUnloaded(object sender, RoutedEventArgs e)
        {
            userClickToItem = false;
        }
    }
}