using System.Security.Principal;
using Locator.Entity.Entities;

namespace LocatorService.Authorization
{
    public class CustomUser : IPrincipal
    {
        public User CurrentUser { get; private set; }

        public CustomUser(User user)
        {
            CurrentUser = user;
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        public IIdentity Identity { get; private set; }
    }
}