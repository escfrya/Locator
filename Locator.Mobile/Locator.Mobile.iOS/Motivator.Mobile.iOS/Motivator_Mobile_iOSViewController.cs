using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Motivator.Mobile.Presentation;
using Motivator.Entity.Entities;
using System.Collections;
using System.Collections.Generic;


namespace Motivator.Mobile.iOS
{
	public partial class Motivator_Mobile_iOSViewController : BaseViewController<MemoriesPresenter>, IMemoriesView
	{
		private MemoriesTableViewSource source;
		private MemoryViewController memoryTaskViewController;

		public void UpdateCallback (List<MemTask> memories)
		{
			ActionsTable.ReloadData ();
		}

		public Motivator_Mobile_iOSViewController () : base ("Motivator_Mobile_iOSViewController")
		{

			Memories = new List<MemTask> ();
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

			Refresh (this, new RefreshEventArgs());	
			// Perform any additional setup after loading the view, typically from a nib.
			source = new MemoriesTableViewSource (this);
			ActionsTable.Source = source;
		}

		[Obsolete]
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}

		public List<MemTask> Memories { get; set; }
		public event EventHandler<EventArgs> AddTask;
		public event EventHandler<OpenTaskArgs> OpenTask;
		public event EventHandler<RefreshEventArgs> Refresh;

		class MemoriesTableViewSource : UITableViewSource
		{
			private Motivator_Mobile_iOSViewController controller;

			public MemoriesTableViewSource(Motivator_Mobile_iOSViewController controller)
			{
				this.controller = controller;
			}
			public override int RowsInSection (UITableView tableview, int section) 
			{ 
				return controller.Memories.Count; 
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				// raise our event
				var handler = controller.OpenTask;
				if (handler != null)
					handler (this, new OpenTaskArgs { TaskId = controller.Memories[indexPath.Row].ID });
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{  
				UITableViewCell cell = new UITableViewCell(UITableViewCellStyle.Default,"mycell"); 
				cell.TextLabel.Text = controller.Memories[indexPath.Row].Text;
				return cell;
			}
		}
	}
}

