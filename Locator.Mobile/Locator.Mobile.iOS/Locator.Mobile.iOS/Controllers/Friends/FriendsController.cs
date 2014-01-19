using System;
using Locator.Mobile.Presentation;
using VI.News.Client.iOS.Base.Controller;
using Locator.ServiceContract.Models;
using Locator.Entity.Entities;
using System.Collections.Generic;
using Locator.Mobile.iOS.Base.Table;
using Locator.Mobile.iOS.Base.Table.BaseCells;
using MonoTouch.CoreLocation;
using MonoTouch.UIKit;

namespace Locator.Mobile.iOS.Controllers.Friends
{
	public class FriendsController : BaseUITableViewController<FriendsPresenter, IFriendsView>, IFriendsView
	{
		public event Action<long, double, double> SendLocation;

		public void Update (FriendsModel model)
		{
			Friends = model;
			var items = new List<Item>();

			if (Friends != null)
			{
				foreach (var friend in Friends.Friends)
				{
					items.AddRange(new Item[]
						{
							new FriendItem(friend)
						});
				}
				SetFriends(items);
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			RaiseRerfresh ();
		}

		private void SetFriends(List<Item> items)
		{
			TableView.Source = new ItemSource(new List<Section>
				{
					new Section
					{
						Items = new List<Item>
						{
							new TextItem { Text = "Friends" }
						}
					},
					new Section
					{
						Items = items
					}
				})
			{
				Touch = n => {
					var item = n as FriendItem;
					if (item != null)
					{
						GetLocation(item.Friend.ID);
					}
				}
			};
			TableView.ReloadData();
		}

		public FriendsModel Friends { get; set; }

		public User Friend { get; set; }

		private void GetLocation(long friendId)
		{
			var locationManager = new CLLocationManager ();
			locationManager.StartUpdatingLocation();

			if (UIDevice.CurrentDevice.CheckSystemVersion (6, 0)) {
				EventHandler<CLLocationsUpdatedEventArgs> handler = null;
				handler = (s, e) => {
					locationManager.LocationsUpdated -= handler;
					var location = e.Locations [e.Locations.Length - 1].Coordinate;
					SendLocation(friendId, location.Latitude, location.Longitude);
					locationManager.StopUpdatingLocation();
				};
				locationManager.LocationsUpdated += handler;
			} else {
				// this won't be called on iOS 6 (deprecated)
				EventHandler<CLLocationUpdatedEventArgs> handler = null;
				handler = (s, e) => {
					locationManager.UpdatedLocation -= handler;
					var location = e.NewLocation.Coordinate;
					SendLocation(friendId, location.Latitude, location.Longitude);
					locationManager.StopUpdatingLocation();
				};
				locationManager.UpdatedLocation += handler;
			}
		}
	}
}

