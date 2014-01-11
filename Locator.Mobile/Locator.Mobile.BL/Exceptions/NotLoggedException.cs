using System;

namespace Locator.Mobile.BL.Exceptions
{
    /// <summary>
    /// Пользователь не авторизован.
    /// </summary>
    public class NotLoggedException : Exception
    {
        public NotLoggedException(string message)
            : base(message)
        {
        }
    }
}
