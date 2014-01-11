using System.Net;

namespace Locator.Mobile.BL.Response
{
    public class RequestResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
    }
}
