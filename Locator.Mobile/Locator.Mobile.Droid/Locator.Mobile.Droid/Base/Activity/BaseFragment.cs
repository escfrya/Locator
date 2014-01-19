using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TinyIoC;
using Locator.Mobile.Presentation;
using Android;

namespace Locator.Mobile.Client.Droid
{
	public class BaseFragment<TPresenter, TView> : Fragment, IBaseView
		where TPresenter : class
		where TView : class, IBaseView
	{
		protected ViewController _fragment;
		protected TinyIoCContainer Container { get; private set; }
		private ProgressBar _progressBar;

		public BaseFragment()
		{
			Container = TinyIoCContainer.Current;
			var container = Container.GetChildContainer ();
			_fragment = new ViewController (this, container, () => { return Activity; });
			_fragment.ConstructPresenter<TPresenter, TView>  ();
			
		}

		protected virtual bool RefreshEveryTime { get { return false; } }

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			if (!RefreshEveryTime)
				InvokeRefresh ();
		}
		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			base.OnViewCreated (view, savedInstanceState);
			if (RefreshEveryTime)
				InvokeRefresh ();
		}

		public virtual void InvokeRefresh()
		{
			_fragment.InvokeRefresh ();
		}

		public override void OnDestroy()
		{
			if (_fragment != null)
				_fragment.Dispose ();
			base.OnDestroy();
		}

		public void Invoke (Action action)
		{
			Activity.RunOnUiThread (action);
		}

		#region IView implementation
		public bool Progress {
			set {
				if (_progressBar != null) {
					if (value) {
						_progressBar.Visibility = ViewStates.Visible;
					} else {
						_progressBar.Visibility = ViewStates.Gone;
					}
				}
			}
		}

		public bool Transfer { set { _fragment.Transfer = value; } }
		public void Lock(string messsage)
		{
			_fragment.Lock (messsage);
		}

		public void Unlock()
		{
			_fragment.Unlock ();
		}

		public event Action Refresh
		{
			add { _fragment.Refresh += value; }
			remove { _fragment.Refresh -= value; }
		}
		public void ShowOfflineAlert ()
		{
			_fragment.ShowOfflineAlert ();
		}
		public bool WasRefreshed { set { _fragment.WasRefreshed = value; } }
		public bool IsFirstLoad { set { _fragment.IsFirstLoad = value; } }
		#endregion

		#region IMessageView implementation	
		public void ShowInfo(string info)
		{
			_fragment.ShowInfo (info);
		}

		public void ShowError(string error)
		{
			_fragment.ShowInfo (error);
		}
		#endregion

		#region IProgressView implementation
		public void ShowLockMessage (string message)
		{
			_fragment.ShowLockMessage (message);
		}
		public void HideLockMessage ()
		{
			_fragment.HideLockMessage ();
		}
		#endregion
	}
}

