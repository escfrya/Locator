// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Motivator.Mobile.iOS
{
	[Register ("MemoryViewController")]
	partial class MemoryViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton AddButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView MemoriesTable { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField MemoryDescription { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField MemoryWithWho { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel TaskText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TaskText != null) {
				TaskText.Dispose ();
				TaskText = null;
			}

			if (MemoriesTable != null) {
				MemoriesTable.Dispose ();
				MemoriesTable = null;
			}

			if (MemoryDescription != null) {
				MemoryDescription.Dispose ();
				MemoryDescription = null;
			}

			if (MemoryWithWho != null) {
				MemoryWithWho.Dispose ();
				MemoryWithWho = null;
			}

			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}
		}
	}
}
