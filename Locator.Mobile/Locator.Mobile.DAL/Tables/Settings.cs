using SQLite;

namespace Locator.Mobile.DAL.Tables
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(int.MaxValue)]
        public string Key { get; set; }

        [MaxLength(int.MaxValue)]
        public string Value { get; set; }
    }
}