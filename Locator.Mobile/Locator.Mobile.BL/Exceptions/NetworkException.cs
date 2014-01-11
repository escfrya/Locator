using System;

namespace Locator.Mobile.BL.Exceptions
{
    /// <summary>
    /// Сетевые проблемы
    /// </summary>
    public class NetworkException : Exception
    {
        public NetworkException(string message)
            : base(message)
        {
        }
    }
}
