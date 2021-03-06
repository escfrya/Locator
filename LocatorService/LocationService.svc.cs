﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using System.Xml.Linq;
using Facebook;
using Locator.DAL.EF;
using Locator.DAL.Repositories;
using Locator.Entity.Entities;
using Locator.ServiceContract;
using Locator.ServiceContract.Models;
using LocatorService.Authorization;
using LocatorService.Cache;

namespace LocatorService
{
    [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Single,
    InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class LocatorService : ILocatorService
    {
        private readonly IUserRepository userRepository;
        private readonly ILocationRepository locationRepository;
        private readonly PushNotificationService pushService;
        private readonly UserPushRepository userPushRepository;

        public LocatorService()
        {
            var context = new LocatorContext();
            userRepository = new UserRepository(context);
            locationRepository = new LocationRepository(context);
            userPushRepository = new UserPushRepository(context);
            pushService = new PushNotificationService(userPushRepository, userRepository);
        }

        [Authorization]
        public void RegisterDevice(DeviceDto device)
        {
            pushService.RegisterDevice(device);
        }

        //[Authorization]
        //[Cache(0)]
        //public void AddFriend(string userId, User user)
        //{
        //    userFriendsRepository.Add(new UserFriends
        //        {
        //            UserId = int.Parse(userId),
        //            FriendId = user.ID
        //        });
        //}

        [Authorization]
        [Cache(0)]
        public LocationsModel GetLocations(string userId)
        {
            var toUserId = GetCurrentUserId();
            var fromUserId = long.Parse(userId);
            var locs = locationRepository.GetByFilter(f => 
                        (f.ToUserId == toUserId && f.FromUserId == fromUserId) ||
                        (f.ToUserId == fromUserId && f.FromUserId == toUserId)
                        ).OrderByDescending(l => l.ID).ToList();
            return new LocationsModel
                {
                    Locations = locs
                };
        }

        [Authorization]
        [Cache(0)]
        public Location GetLocation(string locationId)
        {
            var userId = GetCurrentUserId();
            var location = locationRepository.Get(long.Parse(locationId));
            if (location != null && location.ToUserId == userId)
                return location;
            return null;
        }

        [Authorization]
        [Cache(0)]
        public FriendsModel GetFriends()
        {
            // TODO: get only friends
            var userFriends = userRepository.GetAll();
            return new FriendsModel
                {
                    Friends = userFriends.ToList()
                };
        }

        [Cache(0)]
        [Authorization]
        public void SendLocation(Location location)
        {
            var userId = GetCurrentUserId();
            //var fromUser = userRepository.Get(userId);
            location.Date = DateTime.Now.ToString("hh:mm dd.MM");
            location.FromUserId = userId;
            location.Description = string.Format("{0}", GetLocationDesc(location));
            locationRepository.Add(location);

            pushService.SendNotification(new NotificationPackage
                {
                    Notifications = new List<NotificationDto>
                        {
                            new NotificationDto
                                {
                                    NotificationType = NotificationType.Location,
                                    Message = location.Description,
                                    Count = 1,
                                    UserId = location.ToUserId,
                                    ObjectId = location.FromUserId.ToString()
                                }
                        }
                });
        }

        [Cache(0)]
        [Authorization]
        public void RequestLocation(RequestLocation request)
        {
            var userId = GetCurrentUserId();
            pushService.SendNotification(new NotificationPackage
            {
                Notifications = new List<NotificationDto>
                        {
                            new NotificationDto
                                {
                                    NotificationType = NotificationType.RequestLocation,
                                    Message = "",
                                    UserId = request.UserId,
                                    ObjectId = userId.ToString(),
                                    ContentAvailable = true
                                }
                        }
            });
        }

        private string GetLocationDesc(Location location)
        {
            var desc = string.Empty;
            try
            {
                var requestUri = string.Format("http://maps.google.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", location.Latitude.ToString().Replace(',', '.'), location.Longitude.ToString().Replace(',', '.'));
                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());
                var result = xdoc.Element("GeocodeResponse").Elements("result").FirstOrDefault();
                if (result != null)
                {
                    var element = result.Element("formatted_address");
                    desc = element != null ? element.Value : "";
                }
            }
            catch (Exception exc)
            {
            }
            return desc;
        }

        [Cache(0)]
        public User AddUser(User user)
        {
            
            return userRepository.Add(user);
        }

        private long GetCurrentUserId()
        {
            return ((CustomUser)HttpContext.Current.User).CurrentUser.ID;
        }

        [Cache(0)]
        public RegistrationResponse Registration(RegistrationModel request)
        {
            var response = new RegistrationResponse();
            User fbUser = ValidateFacebookToken(request.Token);
            if (fbUser == null)
            {
                response.Success = false;
                return response;
            }
            var user = userRepository.GetByFilter(u => u.Login == fbUser.Login).FirstOrDefault();
            if (user == null)
            {
                user = userRepository.Add(new User
                    {
                        DisplayName = fbUser.DisplayName,
                        Login = fbUser.Login,
                        AvatarUrl = fbUser.AvatarUrl,
                        Password = ""
                    });
            }
            AuthService.CreateToken(user);
            response.Success = true;
            response.CurrentUser = user;
            return response;
        }


        private User ValidateFacebookToken(string token)
        {
            try
            {
                var client = new FacebookClient(token);
                dynamic me = client.Get("me");
                return new User
                {
                    Login = me.email,
                    DisplayName = me.name,
                    AvatarUrl = string.Format("https://graph.facebook.com/{0}/picture?type=large&height=100&width=100", me.username)
                };
            }
            catch (FacebookOAuthException)
            {
                // Our access token is invalid or expired
            }

            return null;
        }
    }
}
