using System.IO;
using System.Net;
using System.Text;
using Locator.Entity.Entities;

namespace LocatorService
{
    public class PushNotificationManager
    {
        public void SendPush(string pushUrl, string text, string date)
        {
            if (!string.IsNullOrWhiteSpace(pushUrl))
            {
                var sendNotificationRequest = (HttpWebRequest)WebRequest.Create(pushUrl);
                sendNotificationRequest.Method = "POST";
                string pageUrl = string.Format(@"/MainPage.xaml");
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