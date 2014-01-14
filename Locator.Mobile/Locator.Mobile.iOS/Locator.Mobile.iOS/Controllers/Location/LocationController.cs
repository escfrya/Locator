using System;
using Locator.Mobile.Presentation;
using VI.News.Client.iOS.Base.Controller;
using Locator.Entity.Entities;
using XibFree;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;

namespace Locator.Mobile.iOS
{
	public class LocationController : BaseUILayoutViewController<LocationPresenter, ILocationView>, ILocationView
	{
		private MKMapView _mapView;

		public override void LoadView ()
		{
			base.LoadView ();
			Layout.SubViews = new View[] {
				new NativeView () {
					View = _mapView = new MKMapView(),
					LayoutParameters = new LayoutParameters()
					{
						Width = AutoSize.FillParent,
						Height = AutoSize.FillParent,
					}
				}
			};
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Refresh (LocationId);
		}

		public event Action<long> Refresh;

		public long LocationId { get; set; }
		public Location Location { get; set; }

		public void Update (Location location)
		{
			Location = location;
			var coord = new CLLocationCoordinate2D (Location.Latitude, Location.Longitude);
			var point = new MKPointAnnotation {
				Title = Location.Description,
				Coordinate = coord
			};
			_mapView.AddAnnotation (point);

			var mapRegion = MKCoordinateRegion.FromDistance (coord, 100, 100);
			_mapView.CenterCoordinate = coord;
			_mapView.Region = mapRegion;
		}
	}
}

