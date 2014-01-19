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
using Locator.Mobile.Droid;

namespace Locator.Mobile.Client.Droid
{
	public class ViewController : IBaseView, IDisposable
	{
		public TinyIoCContainer Container { get; set; }
		private readonly Context _context;
		private readonly IBaseView _view;

		public ViewController(IBaseView view, TinyIoCContainer container, Func<Context> getContext)
		{
			_context = App.AppContext;
			_view = view;
			Container = container;
			Container.Register<IDispatcher> (this);
			Container.Register<INavigation, Navigations> (new Navigations(getContext));
		}

		public void ConstructPresenter<TPresenter, TView>()
			where TPresenter : class
			where TView : class, IBaseView
		{
				var presenter = Container.Resolve<TPresenter>(new NamedParameterOverloads(new Dictionary<string, object> {
				{"view", _view}
				}));

				Container.Register(presenter);
		}

		public void Invoke (Action action)
		{
			((IDispatcher)_view).Invoke (action);
		}

		public virtual void RefreshAction()
		{
			var call = Refresh;
			if (call != null)
				call ();
		}

		public void InvokeRefresh()
		{
			var call = Refresh;
			if (call != null)
				call ();
		}

		#region IView implementation
		public bool Progress { set{} }
		public bool Transfer { set{} }
		public void Lock(string messsage)
		{

		}

		public void Unlock()
		{

		}

		public event Action Refresh;

		public void ShowOfflineAlert ()
		{

		}
		public bool WasRefreshed { set{} }
		public bool IsFirstLoad { set{} }
		#endregion

		#region IMessageView implementation	
		public void ShowInfo(string info)
		{
			var builder = new AlertDialog.Builder(_context);
			builder.SetTitle("Info");
			builder.SetMessage(info);
			builder.Show();
		}

		public void ShowError(string error)
		{
			var builder = new AlertDialog.Builder(_context);
			builder.SetTitle("Error");
			builder.SetMessage(error);
			builder.Show();
		}
		#endregion

		#region IProgressView implementation
		public void ShowLockMessage (string message)
		{

		}
		public void HideLockMessage ()
		{

		}
		#endregion

		public void Dispose ()
		{
			Container.Dispose ();
		}
	}
	
}
