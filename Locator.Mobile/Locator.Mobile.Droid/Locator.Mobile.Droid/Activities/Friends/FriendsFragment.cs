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
using Locator.Mobile.Presentation;
using Locator.Mobile.Client.Droid;
using Locator.Entity.Entities;
using Locator.ServiceContract.Models;
using Android.Locations;

namespace Locator.Mobile.Droid
{
	public class FriendsFragment : BaseFragment<FriendsPresenter, IFriendsView>, IFriendsView, ILocationListener
	{
		private ListView _friendsList;
		private FriendsAdapter _friendsAdapter;
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.Friends, container, false);
			_friendsList = view.FindViewById<ListView> (Resource.Id.FriendsList);
			_friendsList.ItemClick += ItemClick;

			if (_friendsAdapter != null)
				_friendsList.Adapter = _friendsAdapter;

			return view;
		}

		private long userId;
		private void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var user = (User)((FriendsAdapter)_friendsList.Adapter) [e.Position];
			userId = user.ID;
			GetAndSendLocation ();
		}

		public event Action<long, double, double> SendLocation;
		public void Update (FriendsModel model)
		{
			var items = new List<object> ();
			foreach (var item in model.Friends) {
				items.Add (item);
			}
			_friendsList.Adapter = _friendsAdapter = new FriendsAdapter(Activity, items);
		}
		public FriendsModel Friends { get; set; }
		public User Friend { get; set; }

		#region GetLocation

		private void GetAndSendLocation()
		{
			var locationManager = (LocationManager)Activity.GetSystemService(Context.LocationService);
			Criteria criteria = new Criteria();
			criteria.Accuracy = Accuracy.Fine;

			//_locationProvider = _locationManager.GetBestProvider(criteria, true);
			locationManager.RequestSingleUpdate (LocationManager.NetworkProvider, this, null);
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			SendLocation (userId, location.Latitude, location.Longitude);
		}

		public void OnProviderDisabled (string provider)
		{
		}

		public void OnProviderEnabled (string provider)
		{
		}

		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
		}

		#endregion
	}
}