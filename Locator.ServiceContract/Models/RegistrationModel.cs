using Locator.Entity.Entities;

namespace Locator.ServiceContract.Models
{
    public class RegistrationModel
    {
        public User User { get; set; }
        public string Token { get; set; }
    }

    public class RegistrationResponse
    {
        public bool Success { get; set; }
    }
}
