using System;
using System.Linq;
using Locator.DAL.EF;
using Locator.DAL.Repositories;
using Locator.Entity.Entities;

namespace LocatorService
{
    public class PushHelper
    {
        private readonly IUserPushRepository userPushRepository;

        public PushHelper(LocatorContext context)
        {
            userPushRepository = new UserPushRepository(context);
        }

        public void SendPushToDevices(User user, string text, DateTime date)
        {
            var devices = userPushRepository.GetByFilter(x => x.UserID == user.ID);
            foreach (var device in devices)
            {
                (new PushNotificationManager()).SendPush(device.PushUrl, text, date.ToString("d"));
            }
        }

        public void UpdateUserPushUrl(User user, UserPush userPush)
        {
            var devices = userPushRepository.GetByFilter(x => x.UserID == user.ID);
            if (devices != null)
            {
                var device = devices.FirstOrDefault(x => x.PhoneID == userPush.PhoneID);
                if (device != null)
                {
                    device.PushUrl = userPush.PushUrl;
                    userPushRepository.Update(device);
                }
                else
                {
                    userPushRepository.Add(new UserPush
                                                {
                                                    PhoneID = userPush.PhoneID,
                                                    PushUrl = userPush.PushUrl,
                                                    UserID = user.ID
                                                });
                }
            }
        }
    }
}