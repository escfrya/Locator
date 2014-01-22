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

        private bool AuthenticateUser()
        {

            var ticket = AuthService.GetTicket();
            if (ticket != null)
            {
                var userRepository = new UserRepository(new LocatorContext());
                var user = userRepository.GetByFilter(u => u.Login == ticket.Name).FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Current.User = new CustomUser(user);
                    return true;
                }
            }
            return false;
        }
    }
}