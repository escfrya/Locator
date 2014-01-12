namespace Locator.Entity.Entities
{
    public class UserPush : BaseEntity
    {
        public bool IsActive { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string DeviceAppId { get; set; }
        public string ClientVersion { get; set; }
        public PlatformType PlatformType { get; set; }
        //public string Language { get; set; }
    }

    /// <summary>
    /// Тип платформы клиентского приложения
    /// </summary>
    public enum PlatformType
    {
        iOS = 1,
        Android,
        WindowsPhone
    }
}
