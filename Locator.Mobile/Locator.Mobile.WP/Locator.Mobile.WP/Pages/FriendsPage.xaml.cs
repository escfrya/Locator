using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Locator.Mobile.Presentation;
using Locator.Mobile.WP.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Locator.Mobile.WP.Pages
{
    public partial class FriendsPage : PhoneApplicationPage
    {
        private readonly FriendsViewModel viewModel;
        private bool userClickToItem;
        private bool firstLoad = true;

        public FriendsPage()
        {
            InitializeComponent();
            viewModel = App.Container.Resolve<FriendsViewModel>();
            DataContext = viewModel;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userClickToItem && viewModel.Friend != null)
                viewModel.SendLocationCommand.Execute(null);
        }

        private void PhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            if (firstLoad)
            {
                viewModel.ViewLoad();
                firstLoad = false;
            }
            viewModel.ParseParams(NavigationContext.QueryString);
            viewModel.RefreshCommand.Execute(null);
            userClickToItem = true;
        }

        private void PhoneApplicationPageUnloaded(object sender, RoutedEventArgs e)
        {
            userClickToItem = false;
            //viewModel.ViewUnload();
        }
    }
}