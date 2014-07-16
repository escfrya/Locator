using System.Configuration;
using System.IO;
using System.Web;
using PushSharp;
using PushSharp.Apple;

namespace PushNotifications.NotificationsProvider
{
    public class ApplePushProvider : IPushProvider
    {
        private const string AppleSound = "sound.caf";
        private readonly PushBroker broker;
        public ApplePushProvider(PushBroker broker)
        {
            this.broker = broker;
        }

        public void RegisterPushService()
        {
            var path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AppleCertPath"]);
            var appleCert = File.ReadAllBytes(path);
            var pass = ConfigurationManager.AppSettings["AppleCertPassword"];
            var isProduction = bool.Parse(ConfigurationManager.AppSettings["IsProd"]);
            var a = new ApplePushChannelSettings(isProduction, appleCert, pass);
            broker.RegisterAppleService(a);
        }

        public void SendNotification(NotificationData data)
        {
            var notify = new AppleNotification(data.DeviceAppId);



            if (data.ContentAvailable)
            {
                notify.WithContentAvailable(int.Parse(data.Items["objectId"]))
                      //.WithAlert("remote alert")
                      .WithSound("default");
                //notify.Payload.AddCustom("objectId", data.Items["objectId"]);
                broker.QueueNotification(notify);
                return;
            }
            else
            {
                notify
                    .WithAlert(data.Message)
                    .WithSound(AppleSound);

                if (data.Badge != null)
                    notify.WithBadge(data.Badge.Value);
            }

            notify.ForDeviceToken(data.DeviceAppId)
                  .WithCustomItem("type", data.NotificationType.ToString());

            if (data.Items != null)
                foreach (var item in data.Items)
                    notify.WithCustomItem(item.Key, item.Value);

            broker.QueueNotification(notify);
        }
    }
}