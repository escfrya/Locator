using System.Linq;
using Locator.Mobile.DAL.Tables;

namespace Locator.Mobile.DAL
{
    public interface ISettingsRepository : IBaseRepository<Settings>
    {
    }

    public class SettingsRepository : ISettingsRepository
    {
        private readonly LocalDbContext context;
        private readonly object locker = new object();

        public SettingsRepository()
        {
            context = new LocalDbContext();
        }

        public Settings Get(string id)
        {
            lock (locker)
            {
                return context.Db.Query<Settings>(string.Format("select * from {0} where Key = ?", typeof(Settings).Name), id).FirstOrDefault();
            }
        }

        public Settings Save(Settings entity, string id)
        {
            lock (locker)
            {
                Delete(id);
                context.Db.Insert(entity, typeof(Settings));
                return Get(id);
            }
        }

        public void Delete(string url)
        {
            lock (locker)
            {
                var obj = Get(url);
                if (obj != null)
                    context.Db.Delete(obj);
            }
        }
    }
}