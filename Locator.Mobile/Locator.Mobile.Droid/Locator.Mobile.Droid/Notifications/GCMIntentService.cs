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
using Locator.Mobile.Droid;
using Locator.ServiceContract;
using Locator.Entity.Entities;
using Locator.Mobile.Presentation;

namespace Locator.Mobile.Client.Droid
{
	[Service]
	public class GCMIntentService : IntentService
	{
		private static PowerManager.WakeLock _wakeLock;
		private static object _lock = new object();

		public static void RunIntentInService(Context context, Intent intent)
		{
			lock (_lock)
			{
				if (_wakeLock == null)
				{
					// This is called from BroadcastReceiver, there is no init.
					var pm = PowerManager.FromContext(context);
					_wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "WakeLockTag");
				}
			}

			_wakeLock.Acquire();
			intent.SetClass(context, typeof(GCMIntentService));
			context.StartService(intent);
		}

		protected override void OnHandleIntent(Intent intent)
		{
			try
			{
				Context context = this.ApplicationContext;
				string action = intent.Action;

				if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
				{
					HandleRegistration(context, intent);
				}
				else if (action.Equals("com.google.android.c2dm.intent.RECEIVE"))
				{
					HandleMessage(context, intent);
				}
			}
			finally
			{
				lock (_lock)
				{
					//Sanity check for null as this is a public method
					if (_wakeLock != null)
						_wakeLock.Release();
				}
			}
		}

		void HandleRegistration (Context context, Intent intent)
		{
			var error = intent.GetStringExtra("error");
			string registrationId = intent.GetStringExtra("registration_id");
			RegisterDeviceOnServer (registrationId);
		}

		public void RegisterDeviceOnServer (string token)
		{
			var device = new DeviceDto {
				DeviceAppId = token,
				ClientVersion = new Version(1,0,0,0).ToString(),
				PlatformType = PlatformType.Android
			};
			var container = TinyIoC.TinyIoCContainer.Current;
			container.Register<INavigation, Navigations> (new Navigations(() => { return this.ApplicationContext; }));
			var appController = container.Resolve<AppController >(new NamedParameterOverloads(new Dictionary<string, object> {
				{"dispatcher", null}
			}));

			container.Register(appController);
			appController.RegisterDevice (device);
		}

		private void HandleMessage(Context context, Intent intent)
		{
			var bundle = intent.Extras;
			var msg = bundle.GetString ("alert");
			var objectId = bundle.GetString (BundleKeys.ObjectKey);
			SendNotification (context, msg, objectId);
		}

		private void SendNotification(Context context, string msg, string objectId) 
		{
			var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
			var intent = new Intent (context, typeof(LocationActivity));
			intent.PutExtra (BundleKeys.ObjectKey, objectId).SetAction("OpenLocation");
			PendingIntent contentIntent = PendingIntent.GetActivity(context, 0, intent, 0);

			var builder = new Notification.Builder (context)
				.SetContentTitle ("Locator")
				.SetContentText (msg)
				.SetTicker (msg)
				//.SetAutoCancel (true)
				//.SetSound(Uri.parse("android.resource://"+ context.getPackageName() + "/raw/horn"))
				.SetSmallIcon(Resource.Drawable.Icon)
				.SetContentIntent (contentIntent);

			//builder.SetContentIntent(contentIntent);
			notificationManager.Notify(1, builder.Notification);
		}
	}
}

