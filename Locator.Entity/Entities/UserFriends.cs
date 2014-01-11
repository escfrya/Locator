namespace Locator.Entity.Entities
{
    public class UserFriends : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long FriendId { get; set; } 
        public User Friend { get; set; } 
    }
}
