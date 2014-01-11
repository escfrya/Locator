using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Motivator.Mobile.Presentation;
using TinyIoC;

namespace Motivator.Mobile.iOS
{
	public class BaseViewController<TPresenter> : UIViewController, IDispatcher
		where TPresenter : class
	{
		protected TinyIoCContainer Container;
		public BaseViewController(string viewName) : base(viewName, null)
		{
			//TODO: fix this

			AppDelegate.Container.Register<IDispatcher> (this);

			Container = AppDelegate.Container.GetChildContainer ();

			//Container.Register<INavigation> (new Navigations (this));
			Container.Register<IDispatcher> (this);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Container.Resolve<TPresenter>(new TinyIoC.NamedParameterOverloads(new Dictionary<string, object> {
				{"view", this}
			}));
		}

		public bool IsBusy { get; set; }
		public string BusyText { get; set; }
		public void MessageDialog(string text)
		{
		}
		public bool OkCancelDialog(string text)
		{
			return true;
		}

		public void Invoke (Action action)
		{
			InvokeOnMainThread(() => action());
		}
	}
}

