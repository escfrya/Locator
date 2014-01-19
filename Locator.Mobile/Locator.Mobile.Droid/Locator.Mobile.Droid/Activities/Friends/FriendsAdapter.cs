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
using Locator.Entity.Entities;

namespace Locator.Mobile.Droid
{
	public class FriendsAdapter : BaseListAdapter<object> 
	{
		private List<object> items;
		public FriendsAdapter(Activity context, List<object> items)
			: base(context, items)
		{
			this.items = items;
		}

		public override View GetView(int position, View convertView, ViewGroup parent) 
		{
			View view = null;
			object obj = items.ElementAt(position);
			if(obj is User) {
				User item = obj as User;
				view = Context.LayoutInflater.Inflate(Resource.Layout.FriendCell, null);
				view.FindViewById<TextView>(Resource.Id.FriendName).Text = item.DisplayName;
			} 
			/*else if (obj is NewsSection) {
				var section = obj as NewsSection;
				view = Context.LayoutInflater.Inflate (Resource.Layout.NewsHeader, null);
				view.FindViewById<TextView> (Resource.Id.NewsHeaderLabel).Text = section.Header;
			}*/
			return view;
		}

	}
}

