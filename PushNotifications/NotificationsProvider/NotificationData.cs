using System.Collections.Generic;
using Locator.Entity.Entities;
using Locator.ServiceContract;

namespace PushNotifications.NotificationsProvider
{
    public class NotificationData
    {
        public bool ContentAvailable { get; set; }
        public string FromUserName { get; set; }
        public PlatformType? PlatformType { get; set; }
        public string DeviceAppId { get; set; }
        public string Message { get; set; }
        public int? Badge { get; set; }
        public Dictionary<string, string> Items { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}