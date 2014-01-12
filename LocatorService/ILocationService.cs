using System.ServiceModel;
using System.ServiceModel.Web;
using Locator.Entity.Entities;
using Locator.ServiceContract.Models;

namespace LocatorService
{
    [ServiceContract]
    public interface ILocatorService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/locations/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        LocationsModel GetLocations();

        [OperationContract]
        [WebGet(UriTemplate = "/locations/{locationId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Location GetLocation(string locationId);

        [OperationContract]
        [WebGet(UriTemplate = "/friends/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FriendsModel GetFriends();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/location/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void SendLocation(Location location);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/users/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        User AddUser(User user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/user_push/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void UpdateUserPush(UserPush userPush);

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/users/{userId}/friends/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //void AddFriend(string userId, User user);
    }
}
