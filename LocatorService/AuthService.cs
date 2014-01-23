using System;
using System.Web;
using System.Web.Security;
using Locator.Entity.Entities;

namespace LocatorService
{
    public class AuthService
    {
        private const string CookieName = "_AUTH_LOCATOR";
        public static void CreateToken(User user)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                user.Login,
                DateTime.Now,
                DateTime.Now.AddYears(50),
                true,
                string.Empty,
                FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var authCookie = new HttpCookie(CookieName)
                {
                    Value = encTicket,
                    Expires = DateTime.Now.AddYears(50)
                };
            HttpContext.Current.Response.Cookies.Set(authCookie);

            //var userCookie = new HttpCookie(CookieName, user.Login);
            //userCookie.Expires.AddYears(50);
            //HttpContext.Current.Response.Cookies.Set(userCookie);
        }

        public static string GetUserFromCookie()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies.Get(CookieName);
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                return FormsAuthentication.Decrypt(authCookie.Value).Name;
            
            return null;
        }

        //public static string GetUserFromCookie()
        //{
        //    HttpCookie authCookie = HttpContext.Current.Request.Cookies.Get(CookieName);
        //    if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
        //        return authCookie.Value;

        //    return null;
        //}
    }
}