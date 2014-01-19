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

namespace Locator.Mobile.Client.Droid
{

	public abstract class BaseListAdapter<TEntity> : BaseAdapter<TEntity>
		where TEntity : class
	{
		protected readonly IEnumerable<TEntity> Items;
		protected readonly Activity Context;

		protected BaseListAdapter(Activity context, IList<TEntity> items)
		{
			Items = items;
			Context = context;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override TEntity this[int position] {  
			get { return Items.ElementAt(position); }
		}

		public override int Count {
			get { return Items.Count(); }
		}

		public abstract override View GetView (int position, View convertView, ViewGroup parent);
	}
	
}
