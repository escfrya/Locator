using System.Xml.Serialization;

namespace Locator.Entity.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }

        [XmlIgnore]
        public string Password;
        
        public string DisplayName { get; set; }
    }
}
