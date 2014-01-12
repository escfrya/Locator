namespace PushNotifications.NotificationsProvider
{
    public interface IPushProvider
    {
        void RegisterPushService();
        void SendNotification(NotificationData data);
    }
}
