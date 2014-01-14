using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using TinyIoC;
using Locator.Mobile.Presentation;
using Locator.Mobile.iOS;
using Locator.Mobile.iOS.Base;

namespace VI.News.Client.iOS.Base.Controller
{
	public class ViewController : IBaseView, IDisposable
    {
        private readonly TinyIoCContainer _container;
        private readonly UIViewController _controller;

        private UITableView _tableView;

        public ViewController(UIViewController controller)
        {
            IsModalDialogClosed = false;
            _controller = controller;
            _container = AppDelegate.Container.GetChildContainer();

			//_container.Register<INavigation> (new Navigations ());
			_container.Register<IDispatcher> (this);
        }

        public bool IsModalDialogClosed { get; set; }

        protected UITableView TableView
        {
            get
            {
                if (_tableView != null) return _tableView;
                var tableViewController = _controller as UITableViewController;
                _tableView = tableViewController != null
                    ? tableViewController.TableView
                    : _controller.View.Subviews.OfType<UITableView>().FirstOrDefault();
                return _tableView;
            }
        }


        public UIViewController Controller
        {
            get { return _controller; }
        }

        public TinyIoCContainer Container
        {
            get { return _container; }
        }

        public bool FirstLoad
        {
            set { }
        }

        public void Invoke(Action action)
        {
            _controller.InvokeOnMainThread(() => action());
        }

        public void Dispose()
        {
            if (Container != null)
                Container.Dispose();
        }

        public event Action Refresh;

        public virtual bool Progress
        {
            set { }
        }

        public bool Transfer
        {
            set { UIApplication.SharedApplication.NetworkActivityIndicatorVisible = value; }
        }

        public void HideKeyboardOnTap()
        {
            _controller.View.AddGestureRecognizer(new UITapGestureRecognizer(() => _controller.View.EndEditing(true)));
        }

        public void LoadView()
        {
        }

        public void RaiseRefresh()
        {
            Action call = Refresh;
            if (call != null)
                call();
        }

        public void InvokeRefresh()
        {
            Action call = Refresh;
            if (call != null)
                call();
        }

        public void ViewWillDisappear()
        {
			/*
			bool isDisposing = false;

            UIViewController[] viewControllers = _controller.NavigationController.ViewControllers;
            int len = viewControllers.Length;
            if (len > 1 && viewControllers[len - 2] == _controller)
            {
                isDisposing = false; // не диспозим при переходе в дочерний элемент
            }
            else
            {
				//if (_controller == AppDelegate.Root.TopView)
				//{
				//    isDisposing = true; // диспозим при переключении в меню
				//}
				//else 
				if (_controller.IsMovingFromParentViewController)
                {
                    isDisposing = true; // диспозим при закрытии дочернего окна
                }
                else if (IsModalDialogClosed)
                {
                    isDisposing = true; // диспозим при закрытии модального диалога
                }
            }

            IsModalDialogClosed = false;

            if (isDisposing)
            {
                _controller.Dispose();
			}*/
        }

        public void ConstructPresenter<TPresenter, TView>(object controller)
            where TPresenter : class
			where TView : class, IBaseView
        {
			var presenter = Container.Resolve<TPresenter>(new TinyIoC.NamedParameterOverloads(new Dictionary<string, object> {
				{"view", controller}
			}));

            Container.Register(presenter);
        }

        #region IMessageView implementation

        public void ShowInfo(string message)
        {
            using (var dialog = new UIAlertView("Информация", message, null, "OK"))
            {
                dialog.Show();
            }
        }

        public void ShowError(string message)
        {
            using (var dialog = new UIAlertView("Ошибка", message, null, "OK"))
            {
                dialog.Show();
            }
        }

        #endregion

        #region IProgressView implementation

        public void ShowLockMessage(string message)
        {
            _controller.ResignFirstResponder();
        }

        public void HideLockMessage()
        {
            _controller.ResignFirstResponder();
        }

        #endregion

        #region IView implementation

        public bool WasRefreshed
        {
            set { }
        }

        public bool IsFirstLoad
        {
            set { }
        }

        #endregion
    }
}