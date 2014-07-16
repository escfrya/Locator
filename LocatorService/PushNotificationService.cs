using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Locator.DAL.Repositories;
using Locator.Entity.Entities;
using Locator.ServiceContract;
using LocatorService.Authorization;
using PushNotifications;
using PushNotifications.NotificationsProvider;

namespace LocatorService
{
    public class PushNotificationService
    {
        private const string ObjectIdItemName = "objectId";
        private readonly IUserPushRepository userPushRepository;
        private readonly IUserRepository userRepository;

        public PushNotificationService(IUserPushRepository userPushRepository, IUserRepository userRepository)
        {
            this.userPushRepository = userPushRepository;
            this.userRepository = userRepository;
        }

        public void SendNotification(NotificationPackage package)
        {
            if (!package.Notifications.Any()) return;

            using (var sender = new NotificationSender())
            {
                foreach (var notification in package.Notifications)
                {
                    var user = userRepository.Get(notification.UserId);
                    var userDevices = userPushRepository.GetByFilter(up => up.UserId == user.ID);

                    foreach (var device in userDevices)
                    {
                        var data = new NotificationData
                        {
                            Badge = notification.Count,
                            Message = notification.Message,
                            DeviceAppId = device.DeviceAppId,
                            NotificationType = notification.NotificationType,
                            PlatformType = device.PlatformType,
                            FromUserName = user.DisplayName,
                            ContentAvailable = notification.ContentAvailable
                        };

                        if (!string.IsNullOrEmpty(notification.ObjectId))
                            data.Items = new Dictionary<string, string> { { ObjectIdItemName, notification.ObjectId } };

                        sender.SendPushNotification(data);
                    }
                }
            }
        }

        public void RegisterDevice(DeviceDto device)
        {
            if (device == null || string.IsNullOrEmpty(device.DeviceAppId))
                throw new ArgumentException("Не указан идентификатор устройства");

            var deviceToken = !string.IsNullOrEmpty(device.OldDeviceAppId) ? device.OldDeviceAppId : device.DeviceAppId;
            var userDeviceMap = userPushRepository.GetByFilter(up => up.DeviceAppId == deviceToken).FirstOrDefault();
            if (userDeviceMap == null)
            {
                userDeviceMap = new UserPush
                {
                    PlatformType = device.PlatformType,
                    UserId = ((CustomUser)HttpContext.Current.User).CurrentUser.ID,
                    IsActive = true
                };
            }
            userDeviceMap.DeviceAppId = device.DeviceAppId;
            userDeviceMap.ClientVersion = device.ClientVersion;

            if (userDeviceMap.ID == 0)
                userPushRepository.Add(userDeviceMap);
            else
                userPushRepository.Update(userDeviceMap);
        }

        public void SendPush(string pushUrl, string text, string date)
        {
            if (!string.IsNullOrWhiteSpace(pushUrl))
            {
                var sendNotificationRequest = (HttpWebRequest)WebRequest.Create(pushUrl);
                sendNotificationRequest.Method = "POST";
                string pageUrl = string.Format(@"/Pages/LocationPage.xaml");
                string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<wp:Notification xmlns:wp=\"WPNotification\">" +
                   "<wp:Toast>" +
                        "<wp:Text1>" + text + "</wp:Text1>" +
                        "<wp:Text2>" + date + "</wp:Text2>" +
                        "<wp:Param>" + pageUrl + "</wp:Param>" +
                   "</wp:Toast> " +
                "</wp:Notification>";
                byte[] notificationMessage = new UTF8Encoding().GetBytes(toastMessage);
                //byte[] notificationMessage = Encoding.Default.GetBytes(toastMessage);
                sendNotificationRequest.ContentLength = notificationMessage.Length;
                sendNotificationRequest.ContentType = "text/xml";
                sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "toast");
                sendNotificationRequest.Headers.Add("X-NotificationClass", "2");
                using (Stream requestStream = sendNotificationRequest.GetRequestStream())
                {
                    requestStream.Write(notificationMessage, 0, notificationMessage.Length);
                }
                var response = (HttpWebResponse)sendNotificationRequest.GetResponse();

                //TODO: обработка результата пуш уведомления
                //string notificationStatus = response.Headers["X-NotificationStatus"];
                //string notificationChannelStatus = response.Headers["X-SubscriptionStatus"];
                //string deviceConnectionStatus = response.Headers["X-DeviceConnectionStatus"];
            }
        }
    }

}