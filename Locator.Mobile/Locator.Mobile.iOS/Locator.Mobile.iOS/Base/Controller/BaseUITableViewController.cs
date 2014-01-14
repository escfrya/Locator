using System;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Locator.Mobile.Presentation;

namespace VI.News.Client.iOS.Base.Controller
{
	public abstract class BaseUITableViewController : UITableViewController, IBaseView
    {
        protected readonly ViewController Controller;

        protected BaseUITableViewController() : this(UITableViewStyle.Plain)
        {
        }

        protected BaseUITableViewController(UITableViewStyle withStyle) : base(withStyle)
        {
            Controller = new ViewController(this);
        }

        public virtual bool FirstLoad
        {
            set { Controller.FirstLoad = value; }
        }

        public virtual bool CanRefresh
        {
            get { return true; }
        }

        public void Invoke(Action action)
        {
            Controller.Invoke(action);
        }

        public event Action Refresh
        {
            add { Controller.Refresh += value; }
            remove { Controller.Refresh -= value; }
        }

        public bool WasRefreshed
        {
            set { Controller.WasRefreshed = value; }
        }

        public virtual bool Progress
        {
            set { Controller.Progress = value; }
        }

        public virtual bool Transfer
        {
            set { Controller.Transfer = value; }
        }

        public virtual void InitPresenter()
        {
            Controller.InvokeRefresh();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitPresenter();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            Controller.ViewWillDisappear();
        }

        public override void LoadView()
        {
            base.LoadView();
            Controller.LoadView();
        }

        protected void HideKeyboardOnTap()
        {
            Controller.HideKeyboardOnTap();
        }

        public void RaiseRerfresh()
        {
            Controller.RaiseRefresh();
        }

        protected override void Dispose(bool disposing)
        {
            Controller.Dispose();
            base.Dispose(disposing);
        }

        public override void DismissViewController(bool animated, NSAction completionHandler)
        {
            Controller.IsModalDialogClosed = true;
            base.DismissViewController(animated, completionHandler);
        }

        public override Task DismissViewControllerAsync(bool animated)
        {
            Controller.IsModalDialogClosed = true;
            return base.DismissViewControllerAsync(animated);
        }

        [Obsolete("Deprecated in iOS 6.0, use DismissViewController (bool, NSAction) instead.")]
        public override void DismissModalViewControllerAnimated(bool animated)
        {
            Controller.IsModalDialogClosed = true;
            base.DismissModalViewControllerAnimated(animated);
        }

        #region IMessageView implementation

        public virtual void ShowInfo(string message)
        {
            Controller.ShowInfo(message);
        }

        public virtual void ShowError(string message)
        {
            Controller.ShowError(message);
        }

        #endregion

        #region IProgressView implementation

        public virtual void ShowLockMessage(string message)
        {
            Controller.ShowLockMessage(message);
        }

        public virtual void HideLockMessage()
        {
            Controller.HideLockMessage();
        }

        #endregion

        #region IView implementation

        public bool IsFirstLoad
        {
            set { Controller.IsFirstLoad = value; }
        }

        #endregion
    }

    public abstract class BaseUITableViewController<TPresenter, TView> : BaseUITableViewController
        where TPresenter : class
		where TView : class, IBaseView
    {
        protected BaseUITableViewController()
        {
            Controller.ConstructPresenter<TPresenter, TView>(this);
        }

        protected BaseUITableViewController(UITableViewStyle withStyle) : base(withStyle)
        {
            Controller.ConstructPresenter<TPresenter, TView>(this);
        }
    }
}