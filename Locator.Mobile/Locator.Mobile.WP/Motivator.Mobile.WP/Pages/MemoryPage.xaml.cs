using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Motivator.Mobile.WP.ViewModels;

namespace Motivator.Mobile.WP
{
    public partial class MemoryPage : PhoneApplicationPage
    {
        private readonly MemoriesViewModel viewModel;
        private bool userClickToItem;
        private bool firstLoad = true;

        public MemoryPage()
        {
            InitializeComponent();
            viewModel = App.Container.Resolve<MemoriesViewModel>();
            DataContext = viewModel;
        }

        private void GoToAbout(object sender, GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml", UriKind.RelativeOrAbsolute));
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userClickToItem && viewModel.MemoryTask != null)
                viewModel.OpenTaskCommand.Execute(viewModel.MemoryTask.ID);
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
