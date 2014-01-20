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
using TinyIoC;
using Locator.Mobile.Presentation;

namespace Locator.Mobile.Client.Droid
{
	public interface IContainerActivity
	{
		TinyIoCContainer Container { get; }
	}

	public class BaseActivity : Activity, IContainerActivity, IBaseView
	{
		private const string RecreatedKey = "Recreated";

		public TinyIoCContainer Container { get; private set; }

		protected ViewController Controller;

		public BaseActivity()
		{
			Locator.Mobile.Droid.Bootstrapper.Initialize();
			Container = TinyIoCContainer.Current.GetChildContainer ();
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			OnActivityCreate (savedInstanceState);

			if (savedInstanceState == null || !savedInstanceState.GetBoolean (RecreatedKey, false))
				FirstInitActivity ();

			InvokeRefresh ();
		}

		protected virtual void OnActivityCreate(Bundle savedInstanceState)
		{
			Controller = new ViewController(this, Container, () => {return this;});
		}

		protected virtual void FirstInitActivity()
		{
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			outState.PutBoolean (RecreatedKey, true);
		}

		protected virtual bool NeedAuthorize { get { return true; } }

		public virtual void InvokeRefresh()
		{
			Controller.InvokeRefresh ();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (Controller != null)
				Controller.Dispose ();
		}

		public void Invoke (Action action)
		{
			RunOnUiThread (action);
		}

		#region IView implementation
		public bool Progress { set { Controller.Progress = value; } }
		public bool Transfer { set { Controller.Transfer = value; } }
		public void Lock(string messsage)
		{
			Controller.Lock (messsage);
		}

		public void Unlock()
		{
			Controller.Unlock ();
		}

		public event Action Refresh
		{
			add { Controller.Refresh += value; }
			remove { Controller.Refresh -= value; }
		}
		public void ShowOfflineAlert ()
		{
			Controller.ShowOfflineAlert ();
		}
		public bool WasRefreshed { set { Controller.WasRefreshed = value; } }
		public bool IsFirstLoad { set { Controller.IsFirstLoad = value; } }
		#endregion

		#region IMessageView implementation	
		public virtual void ShowInfo(string info)
		{
			Controller.ShowInfo (info);
		}

		public virtual void ShowError(string error)
		{
			Controller.ShowInfo (error);
		}
		#endregion

		#region IProgressView implementation
		public virtual void ShowLockMessage (string message)
		{
			Controller.ShowLockMessage (message);
		}
		public virtual void HideLockMessage ()
		{
			Controller.HideLockMessage ();
		}
		#endregion
	}

	public class BaseActivity<TPresenter, TView> : BaseActivity
			where TPresenter : class
		where TView : class, IBaseView
	{
		public BaseActivity()
		{
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			Controller.ConstructPresenter<TPresenter, TView>  ();
		}
	}
}
