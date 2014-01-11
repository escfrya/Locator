using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Motivator.Mobile.Presentation;

namespace Motivator.Mobile.iOS
{
	public class Navigations : INavigation
	{

		private readonly UINavigationController controller;
		public Navigations (UINavigationController controller)
		{
			this.controller = controller;
		}

		#region INavigations implementation

		private List<MemoryViewController> controllers = new List<MemoryViewController> ();

		public void Navigate(string pageName, string param = null)
		{
			var newController = new MemoryViewController ();
			controllers.Add (newController);
			controller.PushViewController (newController, true);
		}

		public void Navigate (string pageName, Dictionary<string, int> parameters)
		{
			var newController = new MemoryViewController ();

			if (parameters != null && parameters.ContainsKey("TaskId"))
				newController.TaskId = parameters ["TaskId"];

			controllers.Add (newController);
			controller.PushViewController (newController, true);
		}

		public void GoBack()
		{
			controller.PopViewControllerAnimated (true);
		}
		#endregion
	}
}

