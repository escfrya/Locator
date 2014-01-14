/*using MonoTouch.Foundation;
using MonoTouch.UIKit;
using VI.News.Client.BL.Settings;
using VI.News.Client.Presentation.Application;
using VI.News.Client.Presentation.Application.Logic;
using VI.News.Client.Presentation.Presenters.NewsDetails;
using VI.News.ServiceClient.Clients;
using VI.News.ServiceContract.PushNotificationService;

namespace Locator.Mobile.iOS.Base
{
    public class NotifyRegistration : BaseNotifyRegistration
    {
        private readonly NSString _alertKey = new NSString("alert");
        private readonly NSString _apsKey = new NSString("aps");
        private readonly NSString _badgeKey = new NSString("badge");

        private readonly INavigations _navigations;
        private readonly NSString _objectIdKey = new NSString("objectId");
        private readonly NSString _soundKey = new NSString("sound");
        private readonly NSString _typeKey = new NSString("Type");

        public NotifyRegistration(ISettings settings, IDeviceClient deviceClient, INavigations navigations)
            : base(settings, deviceClient)
        {
            _navigations = navigations;
            _navigations.NotifyRegistration = this;
        }

        public static object Options { get; set; }

        public override void ReadyForRegisterNotifications()
        {
            UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(UIRemoteNotificationType.Alert
                                                                               | UIRemoteNotificationType.Badge |
                                                                               UIRemoteNotificationType.Sound);
            if (Options == null) return;
            ProcessNotification(Options, false);
            Options = null;
        }

        public override void ProcessNotification(object obj, bool showAlert)
        {
            if (obj == null) return;
            NSDictionary options = (obj as NSDictionary) ?? new NSDictionary();

            // распаковываем пуш уведомление
            if (options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
            {
                options = (options[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary) ??
                          new NSDictionary();
            }

            if (options.ContainsKey(_apsKey) && options.ContainsKey(_typeKey))
            {
                string typeStr = options[_typeKey].ToString();
                ApsContent aps = ParsePushAps(options);

                if (typeStr == NotificationType.ApplicationUpdated.ToString())
                {
                    if (showAlert)
                    {
                        string appName =
                            NSBundle.MainBundle.InfoDictionary[new NSString("CFBundleDisplayName")].ToString();
                        var alert = new UIAlertView(appName, aps.Alert, null, "Cancel", new[] {"Update"});

                        alert.Clicked += (s, e) => { if (e.ButtonIndex == 1) _navigations.Settings(null); };
                        alert.Show();
                    }
                    else
                    {
                        _navigations.Settings(null);
                    }
                }
                else if (typeStr == NotificationType.NewNews.ToString())
                {
                    if (!showAlert)
                    {
                        string newsId = options.ContainsKey(_objectIdKey)
                            ? options.ObjectForKey(_objectIdKey).ToString()
                            : null;
                        if (!string.IsNullOrEmpty(newsId))
                        {
                            _navigations.NewsDetails(null, int.Parse(newsId), NewsState.NotLoaded);
                        }
                        else
                        {
                            _navigations.Home(null);
                        }
                    }
                }
            }
        }

        private ApsContent ParsePushAps(NSDictionary options)
        {
            var apsContent = new ApsContent();
            var aps = options.ObjectForKey(_apsKey) as NSDictionary;

            if (aps == null) return apsContent;

            if (aps.ContainsKey(_alertKey))
            {
                apsContent.Alert = aps[_alertKey].ToString();
            }

            if (aps.ContainsKey(_badgeKey))
            {
                string badgeStr = aps[_badgeKey].ToString();
                apsContent.Badge = int.Parse(badgeStr);
            }

            if (aps.ContainsKey(_soundKey))
            {
                apsContent.Sound = aps[_soundKey].ToString();
            }

            return apsContent;
        }

        private class ApsContent
        {
            public string Alert { get; set; }
            public int Badge { get; set; }
            public string Sound { get; set; }
        }
    }
}*/