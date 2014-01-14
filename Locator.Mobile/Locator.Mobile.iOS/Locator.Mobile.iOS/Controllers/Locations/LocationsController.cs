using System;
using VI.News.Client.iOS.Base.Controller;
using Locator.Mobile.Presentation;
using Locator.ServiceContract.Models;
using System.Collections.Generic;
using Locator.Mobile.iOS.Base.Table;
using Locator.Mobile.iOS.Base.Table.BaseCells;

namespace Locator.Mobile.iOS
{
	public class LocationsController : BaseUITableViewController<LocationsPresenter, ILocationsView>, ILocationsView
	{
		public event Action<long> OpenLocation;

		public void Update (LocationsModel model)
		{
			Locations = model;
			var items = new List<Item>();

			if (Locations != null)
			{
				foreach (var location in Locations.Locations)
				{
					items.AddRange(new Item[]
						{
							new LocationItem(location)
						});
				}
				SetLocations(items);
			}
		}

		public Locator.ServiceContract.Models.LocationsModel Locations { get; set; }

		public Locator.Entity.Entities.Location Location { get; set; }


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			RaiseRerfresh ();
		}

		private void SetLocations(List<Item> items)
		{
			TableView.Source = new ItemSource(new List<Section>
				{
					new Section
					{
						Items = new List<Item>
						{
							new TextItem { Text = "Locations" }
						}
					},
					new Section
					{
						Items = items
					}
				})
			{
				Touch = n => {
					var item = n as LocationItem;
					if (item != null)
					{
						OpenLocation(item.Location.ID);
					}
				}
			};
			TableView.ReloadData();
		}
	}
}

