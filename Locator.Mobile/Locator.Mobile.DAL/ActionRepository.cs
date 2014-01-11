using System;
using System.Collections.Generic;
using System.Linq;
using Locator.Mobile.DAL.Tables;

namespace Locator.Mobile.DAL
{

    //public interface IActionRepository : IBaseRepository<LocatorAction>
    //{
    //    List<LocatorAction> GetExpiredActions();
    //}

    //public class ActionRepository : IActionRepository
    //{
    //    private readonly LocalDbContext context;
    //    private readonly object locker = new object();

    //    public ActionRepository()
    //    {
    //        context = new LocalDbContext();
    //    }

    //    public List<LocatorAction> GetExpiredActions()
    //    {
    //        lock (locker)
    //        {
    //            return context.Db.Query<LocatorAction>(string.Format("select * from {0}", typeof(LocatorAction).Name))
    //                .Where(x => x.Success == null 
    //                    && (x.CreatedDate.Date < DateTime.Now.Date 
    //                    || (x.CreatedDate.Date == DateTime.Now.Date && x.CreatedDate.TimeOfDay  < DateTime.Now.TimeOfDay))).ToList();
    //        }
    //    }

    //    public LocatorAction Get(string id)
    //    {
    //        lock (locker)
    //        {
    //            return context.Db.Query<LocatorAction>(string.Format("select * from {0} where ID = ?", typeof(LocatorAction).Name), id).FirstOrDefault();
    //        }
    //    }

    //    public LocatorAction Save(LocatorAction entity, string id)
    //    {
    //        lock (locker)
    //        {
    //            Delete(id);
    //            context.Db.Insert(entity, typeof(LocatorAction));
    //            return Get(id);
    //        }
    //    }

    //    public void Delete(string id)
    //    {
    //        lock (locker)
    //        {
    //            var obj = Get(id);
    //            if (obj != null)
    //                context.Db.Delete(obj);
    //        }
    //    }
    //}
}
