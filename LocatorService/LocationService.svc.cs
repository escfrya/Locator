using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Locator.DAL.EF;
using Locator.DAL.Repositories;
using Locator.Entity.Entities;
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
        private readonly PushHelper pushHelper;

        public LocatorService()
        {
            var context = new LocatorContext();
            userRepository = new UserRepository(context);
            pushHelper = new PushHelper(context);
            locationRepository = new LocationRepository(context);
        }

        [Authorization]
        public void UpdateUserPush(UserPush userPush)
        {
            var user = userRepository.Get(GetCurrentUserId());
            pushHelper.UpdateUserPushUrl(user, userPush);
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
        public LocationsModel GetLocations()
        {
            var userId = GetCurrentUserId();
            return new LocationsModel
                {
                    Locations = locationRepository.GetByFilter(f => f.ToUserId == userId).ToList()
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

        [Authorization]
        public void SendLocation(Location location)
        {
            var userId = GetCurrentUserId();
            location.FromUserId = userId;
            location.Description = "test desc";
            locationRepository.Add(location);
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
    }
}
