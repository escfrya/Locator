using System;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;

namespace LocatorService.Cache
{
    public class CacheAttribute : Attribute, IOperationBehavior, IParameterInspector
    {
        private readonly int maxCacheAge;

        public CacheAttribute(int maxCacheAge)
        {
            this.maxCacheAge = maxCacheAge;
        }

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
            string cacheControlValue = string.Format("max-age={0}", maxCacheAge);
            WebOperationContext.Current.OutgoingResponse.Headers.Add(HttpResponseHeader.CacheControl, cacheControlValue);
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            return new object();
        }

        #endregion
    }
}