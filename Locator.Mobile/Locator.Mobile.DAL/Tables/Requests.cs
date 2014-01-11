using SQLite;

namespace Locator.Mobile.DAL.Tables
{
    public class Requests
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(int.MaxValue)]
        public string RequestUrl { get; set; }

        [MaxLength(int.MaxValue)]
        public string Response { get; set; }
    }
}
