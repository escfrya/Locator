using System.ServiceModel;
using System.ServiceModel.Web;
using Locator.Entity.Entities;
using Locator.ServiceContract;
using Locator.ServiceContract.Models;

namespace LocatorService
{
    [ServiceContract]
    public interface ILocatorService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/locations/user/{userId}/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        LocationsModel GetLocations(string userId);

        //[OperationContract]
        //[WebGet(UriTemplate = "/locations/{locationId}/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //Location GetLocation(string locationId);

        [OperationContract]
        [WebGet(UriTemplate = "/friends/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FriendsModel GetFriends();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/location/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void SendLocation(Location request);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/location/request/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void RequestLocation(RequestLocation request);

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/users/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //User AddUser(User user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/user_push/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void RegisterDevice(DeviceDto request);

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/users/{userId}/friends/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //void AddFriend(string userId, User user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/registration/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RegistrationResponse Registration(RegistrationModel request);
    }
}
