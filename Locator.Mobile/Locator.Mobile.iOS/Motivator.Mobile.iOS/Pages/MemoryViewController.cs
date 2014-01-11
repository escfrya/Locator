using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Motivator.Mobile.Presentation;
using Motivator.Entity.Entities;
using Motivator.ServiceContract.Models;

namespace Motivator.Mobile.iOS
{
	public partial class MemoryViewController : BaseViewController<MemoryTaskPresenter>, IMemoryTaskView
	{
		private MemoriesTableViewSource source;

		public int TaskId { get; set; }

		public MemoryViewController () : base ("MemoryViewController")
		{
			Memories = new List<Memory> (); 
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			Refresh (this, new RefreshEventArgs { ID = TaskId });	
			// Perform any additional setup after loading the view, typically from a nib.
			source = new MemoriesTableViewSource (this);
			MemoriesTable.Source = source;
		}

		public MemTask Task { get; set; }
		public List<Memory> Memories { get; set; }
		public Memory NewMemory { get; set; }
		public event EventHandler<EventArgs> GetNewTask;
		public event EventHandler<NewMemoryArgs> AddMemory;
		public event EventHandler<EventArgs> Forgot;
		public event EventHandler<RefreshEventArgs> Refresh;

		public void UpdateCallback (MemoryModel model)
		{
			MemoriesTable.ReloadData ();
			TaskText.Text = model.MemTask.Text;
		}

		class MemoriesTableViewSource : UITableViewSource
		{
			private MemoryViewController controller;

			public MemoriesTableViewSource(MemoryViewController controller)
			{
				this.controller = controller;
			}
			public override int RowsInSection (UITableView tableview, int section) 
			{ 
				return controller.Memories.Count; 
			}

			/*public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				// raise our event
				var handler = SongSelected;
				if (handler != null)
					handler (this, new SongSelectedEventArgs (rows[indexPath.Row]));
			}*/

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{  
				UITableViewCell cell = new UITableViewCell(UITableViewCellStyle.Default,"mycell"); 
				cell.TextLabel.Text = controller.Memories[indexPath.Row].Description;
				return cell;
			}
		}
	}
}

