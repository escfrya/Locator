using System.Configuration;
using PushSharp;
using PushSharp.Android;

namespace PushNotifications.NotificationsProvider
{
    public class AndroidPushProvider : IPushProvider
    {
        private const string AndroidSound = "sound.caf";
        private readonly PushBroker broker;
        public AndroidPushProvider(PushBroker broker)
        {
            this.broker = broker;
        }
        public void RegisterPushService()
        {
            broker.RegisterGcmService(new GcmPushChannelSettings(ConfigurationManager.AppSettings["AndroidApiKey"]));
        }

        public void SendNotification(NotificationData data)
        {
            //IMPORTANT: For Android you MUST use your own RegistrationId here that gets generated within your Android app itself!
            broker.QueueNotification(new GcmNotification()
                                          .ForDeviceRegistrationId(data.DeviceAppId)
                                          .WithJson(string.Format("{{\"alert\":\"{0}\",\"badge\":{1},\"sound\":\"{2}\"}}", data.Message, data.Badge.HasValue ? data.Badge : 0, AndroidSound))
                                          .WithData(data.Items));
        }
    }
}