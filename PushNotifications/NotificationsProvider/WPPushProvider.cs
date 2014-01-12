using System;
using PushSharp;
using PushSharp.WindowsPhone;

namespace PushNotifications.NotificationsProvider
{
    public class WPPushProvider : IPushProvider
    {
        private readonly PushBroker broker;
        public WPPushProvider(PushBroker broker)
        {
            this.broker = broker;
        }

        public void RegisterPushService()
        {
            broker.RegisterWindowsPhoneService();
        }

        public void SendNotification(NotificationData data)
        {
            var notify = new WindowsPhoneToastNotification()
                .ForEndpointUri(new Uri(data.DeviceAppId))
                .ForOSVersion(WindowsPhoneDeviceOSVersion.Eight)
                .WithBatchingInterval(BatchingInterval.Immediate)
                .WithNavigatePath("/Pages/LocationPage.xaml")
                .WithText1(data.Message)
                .WithText2(data.FromUserName);
            foreach (var item in data.Items)
            {
                notify.WithParameter(item.Key, item.Value);
            }
            broker.QueueNotification(notify);
        }
    }
}