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
using Locator.Entity.Entities;
using Locator.ServiceContract.Models;

namespace Locator.Mobile.Droid
{
	class LocationsFragment : BaseFragment<LocationsPresenter, ILocationsView>, ILocationsView
	{
		private ListView _locationsList;
		private LocationsAdapter _locationsAdapter;
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.Locations, container, false);
			_locationsList = view.FindViewById<ListView> (Resource.Id.LocationsList);
			_locationsList.ItemClick += ItemClick;

			if (_locationsAdapter != null)
				_locationsList.Adapter = _locationsAdapter;

			return view;
		}

		public override void OnStart ()
		{
			base.OnStart ();
			//InvokeRefresh ();
		}

		private void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var location = (Location)((LocationsAdapter)_locationsList.Adapter) [e.Position];
			OpenLocation (location.ID);
		}

		public event Action<long> OpenLocation;
		public void Update (LocationsModel model)
		{
			var items = new List<object> ();
			foreach (var item in model.Locations) {
				items.Add (item);
			}
			_locationsList.Adapter = _locationsAdapter = new LocationsAdapter(Activity, items);
		}
		public Locator.ServiceContract.Models.LocationsModel Locations { get; set; }
		public Locator.Entity.Entities.Location Location { get; set; }
	}
}

