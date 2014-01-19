using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Locator.Mobile.Client.Droid;
using Locator.Mobile.Presentation;
using Android.GoogleMaps;
using Locator.Entity.Entities;

namespace Locator.Mobile.Droid
{
	[Activity (Label = "Locator")]			
	public class LocationActivity : BaseActivity<LocationPresenter, ILocationView> , ILocationView
	{
		private MapView _mapView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Location);
			//_mapView = FindViewById<MapView> (Resource.Id.MapView);
			LocationId = long.Parse(Intent.GetStringExtra (BundleKeys.ObjectKey));

		}
		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			Refresh (LocationId);
		}
		public event Action<long> Refresh;

		public void Update (Location location)
		{
			var geoUri = Android.Net.Uri.Parse (string.Format("geo:{0},{1}", location.Latitude, location.Longitude));
			var mapIntent = new Intent (Intent.ActionView, geoUri);
			StartActivity (mapIntent);

			//var geoPoint = new GeoPoint ((int)location.Latitude, (int)location.Longitude);
			//_mapView.Controller.SetZoom (19);
			//_mapView.Controller.SetCenter (geoPoint);
		}

		public long LocationId { get; set; }

		public Locator.Entity.Entities.Location Location { get; set; }
	}
}

