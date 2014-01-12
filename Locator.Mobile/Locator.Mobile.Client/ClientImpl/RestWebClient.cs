using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Locator.Mobile.BL;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Response;

namespace Locator.Mobile.Client.ClientImpl
{
    public class RestWebClient : RequestClient
    {
        private const int Timeout = 60000;

        //private const string serviceUrl = "http://localhost:12804/LocationService.svc";
        private const string serviceUrl = "http://motivator.apphb.com/LocationService.svc";

        public RestWebClient(ISerializer serializer)
            : base(serializer)
        {
        }

        protected override RequestResponse Execute(ExecuteParams param)
        {
            var request = (HttpWebRequest) HttpWebRequest.Create(serviceUrl + "/" + param.Address);
            request.Accept = "application/json";
            
            request.Headers["Auth"] = UserSettings.AuthStr;
            var json = string.Empty;

            EventWaitHandle wait = new AutoResetEvent(false);
            RequestResponse response = null;

            switch (param.Type)
            {
                case HTTPType.GET:
                    request.Method = "GET";
                    request.BeginGetResponse(rr =>
                    {
                        var resp = (HttpWebResponse)request.EndGetResponse(rr);
                        Stream dataStream = resp.GetResponseStream();
                        var reader = new StreamReader(dataStream);
                        var content = reader.ReadToEnd();
                        reader.Close();
                        dataStream.Close();
                        response = new RequestResponse
                        {
                            StatusCode = resp.StatusCode,
                            Content = content
                        };
                        wait.Set();
                    }, request);
                    break;
                case HTTPType.POST:
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    json = Serializer.Serialize(param.Request);
                    request.BeginGetRequestStream(r =>
                        {
                            var req = (HttpWebRequest) r.AsyncState;
                            var postStream = req.EndGetRequestStream(r);
                            
                            byte[] byteArray = Encoding.UTF8.GetBytes(json);
                            postStream.Write(byteArray, 0, byteArray.Length);
                            postStream.Flush();
                            postStream.Close();
                            
                            req.BeginGetResponse(rr =>
                                {
                                    var resp = (HttpWebResponse) req.EndGetResponse(rr);
                                    Stream dataStream = resp.GetResponseStream();
                                    var reader = new StreamReader(dataStream);
                                    var content = reader.ReadToEnd();
                                    reader.Close();
                                    dataStream.Close();
                                    response = new RequestResponse
                                        {
                                            StatusCode = resp.StatusCode,
                                            Content = content
                                        };
                                    wait.Set();
                                }, req);
                        }, request);
                    break;
            }

            

            
            wait.WaitOne(Timeout);
            return response;
        }

    }
}
