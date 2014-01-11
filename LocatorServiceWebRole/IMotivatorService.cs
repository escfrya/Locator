using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Motivator.Entity.Entities;

namespace MotivatorServiceWebRole
{
    [ServiceContract]
    public interface IMotivatorService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/actions/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<MotivAction> GetActions();

        [OperationContract]
        [WebGet(UriTemplate = "/actions/{actionId}/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        MotivAction GetAction(string actionId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/actions/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MotivAction AddAction(MotivAction action);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/action/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MotivAction UpdateAction(MotivAction action);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/users/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        User AddUser(User user);
    }
}
