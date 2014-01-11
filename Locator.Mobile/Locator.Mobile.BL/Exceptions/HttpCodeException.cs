using System;
using System.Net;

namespace Locator.Mobile.BL.Exceptions
{
    /// <summary>
    /// Ошибка web-сервера
    /// </summary>
    public class HttpCodeException : Exception
    {
        private readonly HttpStatusCode code;
        
        public HttpCodeException(HttpStatusCode code, string message)
            : base(message)
        {
            this.code = code;
        }

        public HttpStatusCode Code { get { return code; } }
    }
}
