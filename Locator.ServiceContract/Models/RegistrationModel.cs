using Locator.Entity.Entities;

namespace Locator.ServiceContract.Models
{
    public class RegistrationModel
    {
        //public string Login { get; set; }

        //public string Password { get; set; }

        //public string DisplayName { get; set; }

        public string Token { get; set; }
    }

    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public User CurrentUser { get; set; }
    }
}
