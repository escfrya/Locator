using System;
using System.Collections.Generic;
using Locator.Entity.Entities;
using PushNotifications.NotificationsProvider;
using PushSharp;
using PushSharp.Core;
using PushSharp.WindowsPhone;
using PushSharp.Apple;

namespace PushNotifications
{
    public class NotificationSender : IDisposable
    {
        private readonly PushBroker pushBroker;
        private readonly IDictionary<PlatformType, Lazy<IPushProvider>> providersFactory;

        public NotificationSender()
        {

            pushBroker = new PushBroker();
            SubscribeEvents();
            providersFactory = new Dictionary<PlatformType, Lazy<IPushProvider>>
            {
                {PlatformType.iOS, new Lazy<IPushProvider>(() => RegisterPushService(new ApplePushProvider(pushBroker)))},
                {PlatformType.Android, new Lazy<IPushProvider>(() => RegisterPushService(new AndroidPushProvider(pushBroker)))},
                {PlatformType.WindowsPhone, new Lazy<IPushProvider>(() => RegisterPushService(new WPPushProvider(pushBroker)))}
            };
        }

        private void SubscribeEvents()
        {
            pushBroker.OnNotificationSent += PushBrokerOnOnNotificationSent;
            pushBroker.OnChannelException += ChannelException;
            pushBroker.OnServiceException += ServiceException;
            pushBroker.OnNotificationFailed += NotificationFailed;
            pushBroker.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            pushBroker.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
        }

        private void PushBrokerOnOnNotificationSent(object sender, INotification notification)
        {
            
        }

        private static IPushProvider RegisterPushService(IPushProvider provider)
        {
            provider.RegisterPushService();
            return provider;
        }

        public void SendPushNotification(NotificationData data)
        {
            if (data.PlatformType == null)
                throw new ArgumentException("PlatformType is null");
            providersFactory[data.PlatformType.Value].Value.SendNotification(data);
        }

        #region Event processing

        private void DeviceSubscriptionChanged(object sender, string oldsubscriptionid, string newsubscriptionid, INotification notification)
        {
            //_log.Info(string.Format("Subscription changed: oldId = {0}, newId = {1}", oldsubscriptionid, newsubscriptionid));
        }

        private void DeviceSubscriptionExpired(object sender, string expiredsubscriptionid, DateTime expirationdateutc, INotification notification)
        {
            //_log.Info(string.Format("Subscription expired: expiredId = {0}, expiredDateUtc = {1}", expiredsubscriptionid, expirationdateutc));
        }

        private void NotificationFailed(object sender, INotification notification, Exception error)
        {
            var er = error as WindowsPhoneNotificationSendFailureException;
            var er1 = error as NotificationFailureException;
            //_log.Error("NotificationFailed", error);
        }

        private void ServiceException(object sender, Exception error)
        {
            //_log.Error("ServiceException", error);
        }

        private void ChannelException(object sender, IPushChannel pushchannel, Exception error)
        {
            //_log.Error("ChannelException", error);
        }

        #endregion

        public void Dispose()
        {
            //pushBroker.StopAllServices();
        }
    }

}
