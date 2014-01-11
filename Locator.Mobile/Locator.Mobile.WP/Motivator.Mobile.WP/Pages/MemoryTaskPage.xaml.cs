using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Motivator.Mobile.WP.ViewModels;

namespace Motivator.Mobile.WP.Pages
{
    public partial class NewMemoryTask : PhoneApplicationPage
    {
        //private readonly MapOverlay mapOverlay = new MapOverlay();
        private readonly MemoryTaskViewModel viewModel;
        private bool firstLoad = true;
        private bool userClickToItem;
        //private const int ImgSize = 70;

        public NewMemoryTask()
        {
            InitializeComponent();
            viewModel = App.Container.Resolve<MemoryTaskViewModel>();
            DataContext = viewModel;
        }

        private void PhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            if (firstLoad)
            {
                viewModel.ViewLoad();
                firstLoad = false;
            }
            viewModel.ParseParams(NavigationContext.QueryString);
            userClickToItem = true;
            //SetStartLocationMarker();
        }
        private void PhoneApplicationPageUnloaded(object sender, RoutedEventArgs e)
        {
            userClickToItem = false;
        }

        //private async void SetStartLocationMarker()
        //{
        //    FormMarker();
        //    var position = await (new GeoHelper()).GetGeoPosition();
        //    var nowCoord = new GeoCoordinate(position.Coordinate.Latitude, position.Coordinate.Longitude);
        //    Map.Center = nowCoord;
        //    mapOverlay.GeoCoordinate = nowCoord;
        //    SetActionCoord(nowCoord);
        //}

        //private void FormMarker()
        //{
        //    var layer = new MapLayer { mapOverlay };
        //    Map.Layers.Clear();
        //    Map.Layers.Add(layer);
        //    var content = new Border
        //    {
        //        Child = new Image
        //        {
        //            Source = new BitmapImage(new Uri("/Images/appbar.location.round.png", UriKind.Relative)),
        //            Height = ImgSize,
        //            Width = ImgSize,
        //            Margin = new Thickness(-ImgSize / 2, -ImgSize + 10, 0, 0)
        //        }
        //    };
        //    mapOverlay.Content = content;
        //}

        //private void MapOnTap(object sender, GestureEventArgs e)
        //{
        //    var position = e.GetPosition(Map);
        //    var coord = Map.ConvertViewportPointToGeoCoordinate(position);
        //    mapOverlay.GeoCoordinate = coord;
        //    SetActionCoord(coord);
        //}

        //private void SetActionCoord(GeoCoordinate coord)
        //{
        //    viewModel.Action.Latitude = (float)coord.Latitude;
        //    viewModel.Action.Longitude = (float)coord.Longitude;
        //}

    }
}