using System.Linq;
using Locator.Mobile.DAL.Tables;

namespace Locator.Mobile.DAL
{
    public interface IRequestsRepository : IBaseRepository<Requests>
    {
    }

    public class RequestsRepository : IRequestsRepository
    {
        private readonly LocalDbContext context;
        private readonly object locker = new object();

        public RequestsRepository()
        {
            context = new LocalDbContext();
        }

        public Requests Get(string url)
        {
            lock (locker)
            {
                return context.Db.Query<Requests>(string.Format("select * from {0} where RequestUrl = ?", typeof(Requests).Name), url).FirstOrDefault();
            }
        }

        public Requests Save(Requests entity, string url)
        {
            lock (locker)
            {
                Delete(url);
                context.Db.Insert(entity, typeof(Requests));
                return Get(url);
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
