using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Locator.DAL.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        TEntity Add(TEntity entity);
        TEntity Get(long entityId);
        IEnumerable<TEntity> GetAll();
        void Delete(TEntity entity);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
    }
}
