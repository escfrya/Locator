using System.Device.Location;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Locator.Mobile.WP.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;

namespace Locator.Mobile.WP.Pages
{
    public partial class LocationPage : PhoneApplicationPage
    {
        private readonly LocationViewModel viewModel;
        private bool firstLoad = true;

        public LocationPage()
        {
            InitializeComponent();
            viewModel = App.Container.Resolve<LocationViewModel>();
            viewModel.LocationUpdated += ViewModelOnLocationUpdated;
        }

        private void ViewModelOnLocationUpdated()
        {
            var layer = new MapLayer();
            var content = new Border
            {
                BorderThickness = new Thickness(2),
                Height = 5,
                Width = 5,
                BorderBrush = new SolidColorBrush(Colors.Green),
                //Child = new Image
                //        {
                //            Source = new BitmapImage(new Uri(commit.SmallImageUrl)),
                //            Height = MiniatureSize,
                //            Width = MiniatureSize
                //        }
            };
            var overlay = new MapOverlay
            {
                GeoCoordinate = new GeoCoordinate(viewModel.Location.Latitude, viewModel.Location.Longitude),
                Content = content
            };
            layer.Add(overlay);

            Map.Layers.Clear();
            Map.Layers.Add(layer);
            Map.SetView(overlay.GeoCoordinate, 12);
        }

        private void PhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            if (firstLoad)
            {
                viewModel.ViewLoad();
                firstLoad = false;
            }

            viewModel.LocationId = long.Parse(NavigationContext.QueryString["ObjectId"]);
            viewModel.RefreshCommand.Execute(null);
        }

        private void PhoneApplicationPageUnloaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}