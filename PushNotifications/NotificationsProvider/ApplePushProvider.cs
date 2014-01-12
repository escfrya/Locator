using System.Configuration;
using System.IO;
using System.Web;
using PushSharp;
using PushSharp.Apple;

namespace PushNotifications.NotificationsProvider
{
    public class ApplePushProvider : IPushProvider
    {
        private const bool IsProduction = false;
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
            broker.RegisterAppleService(new ApplePushChannelSettings(IsProduction, appleCert, ConfigurationManager.AppSettings["AppleCertPassword"]));
        }

        public void SendNotification(NotificationData data)
        {
            var notify = new AppleNotification()
                .ForDeviceToken(data.DeviceAppId)
                .WithAlert(data.Message)
                .WithSound(AppleSound)
                .WithCustomItem("Type", data.NotificationType.ToString());

            if (data.Badge != null)
                notify.WithBadge(data.Badge.Value);

            if (data.Items != null)
                foreach (var item in data.Items)
                    notify.WithCustomItem(item.Key, item.Value);

            broker.QueueNotification(notify);
        }
    }
}