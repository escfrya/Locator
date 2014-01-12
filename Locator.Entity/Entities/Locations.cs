using System;

namespace Locator.Entity.Entities
{
    public class Location : BaseEntity
    {
        public long FromUserId { get; set; }
        public User FromUser { get; set; }

        public long ToUserId { get; set; }
        public User ToUser { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        //public DateTime Date { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
