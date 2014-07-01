namespace Locator.Entity.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }
    }
}
