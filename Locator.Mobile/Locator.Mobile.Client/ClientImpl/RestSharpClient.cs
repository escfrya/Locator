/*using System.Threading;
using Locator.Mobile.BL;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Response;
using RestSharp;
using ISerializer = Locator.Mobile.BL.Client.ISerializer;

namespace Locator.Mobile.Client.ClientImpl
{
    public class RestSharpClient : RequestClient
    {
        private const int Timeout = 60000;
        private readonly RestClient client;

        public RestSharpClient(ISerializer serializer)
            : base(serializer)
        {
            //client = new RestClient("http://localhost.fiddler:12804/LocatorService.svc");
            client = new RestClient("http://Locator.apphb.com/LocatorService.svc");
            
            //client.CookieContainer = new CookieContainer();
        }

        protected override RequestResponse Execute(ExecuteParams param)
        {
            var request = new RestRequest(param.Address);
            client.DefaultParameters.Clear();
            client.AddDefaultParameter("Accept", "application/json, application/xml, text/json, text/x-json, text/javascript, text/xmls", ParameterType.HttpHeader);

            switch (param.Type)
            {
                case HTTPType.GET:
                    request.Method = Method.GET;
                    break;
                case HTTPType.POST:
                    request.Method = Method.POST;
                    break;
            }

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Auth", UserSettings.AuthStr);
            //request.AddHeader("Cache-Control", "no-cache");
            request.JsonSerializer = (RestSharp.Serializers.ISerializer)Serializer;

            if (param.ByteArray != null)
            {
                request.AddFile("stream",
                    param.ByteArray,
                    "photo.jpg",
                    "image/jpeg");
            }
            else
            {
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "");
                request.AddBody(param.Request);
            }


#if WINDOWS_PHONE
            EventWaitHandle wait = new AutoResetEvent(false);
            RequestResponse response = null;
            client.ExecuteAsync(request, result =>
            {
                if (result.ResponseStatus == ResponseStatus.Completed)
                {
                    response = new RequestResponse
                    {
                        Content = result.Content,
                        StatusCode = result.StatusCode
                    };
                }

                wait.Set();
            });
            wait.WaitOne(Timeout);
            return response;

#else
            var response = client.Execute(request);
            return new RequestResponse
            {
                StatusCode = response.StatusCode,
                Content = response.Content
            };
#endif

        }
    }
}
*/