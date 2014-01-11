namespace Locator.Entity.Entities
{
    public class UserPush : BaseEntity
    {
        public long UserID { get; set; }
        public User User { get; set; }
        public string PhoneID { get; set; }
        public string PushUrl { get; set; }
    }
}
