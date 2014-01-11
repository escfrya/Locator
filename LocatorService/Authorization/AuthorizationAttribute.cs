using System;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Web;
using Locator.DAL.EF;
using Locator.DAL.Repositories;

namespace LocatorService.Authorization
{
    public class AuthorizationAttribute : Attribute, IOperationBehavior, IParameterInspector
    {
        #region AuthCheck
        private bool AuthenticateUser()
        {
            WebOperationContext ctx = WebOperationContext.Current;
            string requestUri = ctx.IncomingRequest.UriTemplateMatch.RequestUri.ToString();
            string authHeader = ctx.IncomingRequest.Headers["Auth"];
            // if supplied hash is valid, user is authenticated
            if (IsValidUserKey(authHeader, requestUri))
                return true;
            return false;
        }
        public bool IsValidUserKey(string key, string uri)
        {
            if (!string.IsNullOrEmpty(key))
            {
                string[] authParts = key.Split(':');
                if (authParts.Length == 2)
                {
                    string userid = authParts[0];
                    string hash = authParts[1];
                    if (ValidateHash(userid, uri, hash))
                        return true;
                }
            }
            return false;
        }
        private bool ValidateHash(string userLogin, string uri, string hash)
        {
            var userRepository = new UserRepository(new LocatorContext());
            var users = userRepository.GetByFilter(u => u.Login == userLogin);
            if (users == null)
                return false;
            var user = users.FirstOrDefault();
            var res = false;
            if (user != null)
            {
                res = user.Password == hash;
                if (res)
                {
                    HttpContext.Current.User = new CustomUser(user);
                }
            }
            return res;

            //byte[] secretBytes = ASCIIEncoding.ASCII.GetBytes(userkey);
            //HMACMD5 hmac = new HMACMD5(secretBytes);
            //byte[] dataBytes = ASCIIEncoding.ASCII.GetBytes(uri);
            //byte[] computedHash = hmac.ComputeHash(dataBytes);
            //string computedHashString = Convert.ToBase64String(computedHash);
            //return computedHashString.Equals(hash);
        }
        #endregion
        public bool IsAuthenticationRequired { get; set; }

        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {

        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        public void Validate(OperationDescription operationDescription)
        {

        }

        #endregion

        #region IParameterInspector Members

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {

        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            if (!AuthenticateUser())
            {
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
            return null;
        }

        #endregion

    }

}