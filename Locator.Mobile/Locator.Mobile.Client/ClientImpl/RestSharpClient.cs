using System.Threading;
using System.Linq;
using Locator.Mobile.BL;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Response;
using Locator.Mobile.DAL;
using Locator.Mobile.DAL.Tables;
using RestSharp;
using ISerializer = Locator.Mobile.BL.Client.ISerializer;

namespace Locator.Mobile.Client.ClientImpl
{
    public class RestSharpClient : RequestClient
    {
        private const int Timeout = 60000;
        private readonly RestClient client;
        private readonly ISettingsRepository settingsRepository;
        public RestSharpClient(ISerializer serializer, ISettingsRepository settingsRepository)
            : base(serializer)
        {
            this.settingsRepository = settingsRepository;

            client = new RestClient("http://192.168.1.77/LocatorService/LocationService.svc");
            //client = new RestClient("http://motivator.apphb.com/LocationService.svc");
            
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
            request.JsonSerializer = (RestSharp.Serializers.ISerializer)Serializer;
            AddAuthCookie(request);
            
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
                    SaveAuthCookie(param.Address, result);

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
            SaveAuthCookie(param.Address, response);
            return new RequestResponse
            {
                StatusCode = response.StatusCode,
                Content = response.Content
            };
#endif

        }

        #region AuthCookie

        private void SaveAuthCookie(string address, IRestResponse response)
        {
            if (address != "registration/")
                return;

            var authCookie = response.Cookies.FirstOrDefault(c => c.Name == AuthCookieName);
            if (authCookie != null)
                settingsRepository.Save(new Settings { Key = AuthCookieName, Value = authCookie.Value }, AuthCookieName);
        }
        private void AddAuthCookie(IRestRequest request)
        {
            var auth = settingsRepository.Get(AuthCookieName);
            if (auth != null)
                request.AddCookie(auth.Key, auth.Value);
        }

        #endregion
    }
}
