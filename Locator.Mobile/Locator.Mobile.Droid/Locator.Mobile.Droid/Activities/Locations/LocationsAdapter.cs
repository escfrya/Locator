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
using Locator.Entity.Entities;
using Locator.Mobile.Client.Droid;

namespace Locator.Mobile.Droid
{
	[Activity (Label = "Locator")]			
	public class LocationsAdapter : BaseListAdapter<object> 
	{
		private List<object> items;
		public LocationsAdapter(Activity context, List<object> items)
			: base(context, items)
		{
			this.items = items;
		}

		public override View GetView(int position, View convertView, ViewGroup parent) 
		{
			View view = null;
			object obj = items.ElementAt(position);
			if(obj is Location) {
				Location item = obj as Location;
				view = Context.LayoutInflater.Inflate(Resource.Layout.LocationCell, null);
				view.FindViewById<TextView>(Resource.Id.LocationDesc).Text = item.Description;
			} 
			/* else if (obj is NewsSection) {
				var section = obj as NewsSection;
				view = Context.LayoutInflater.Inflate (Resource.Layout.NewsHeader, null);
				view.FindViewById<TextView> (Resource.Id.NewsHeaderLabel).Text = section.Header;
			}*/
			return view;
		}
	}
}

