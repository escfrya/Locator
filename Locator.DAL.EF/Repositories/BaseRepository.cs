using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Locator.DAL.Repositories;
using Locator.Entity;

namespace Locator.DAL.EF.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly LocatorContext Context;

        private DbSet<TEntity> dbSet;

        protected DbSet<TEntity> DbSet
        {
            get { return dbSet ?? (dbSet = Context.Set<TEntity>()); }
        }

        private DbQuery<TEntity> getDbSet;
        protected DbQuery<TEntity> GetDbSet
        {
            get { return getDbSet ?? (getDbSet = DbSetWithInclude()); }
        }

        private DbQuery<TEntity> DbSetWithInclude()
        {
            var result = DbSet;
            foreach (var prop in Props)
            {
                result.Include(prop);
            }
            return result;
        }

        protected virtual IEnumerable<string> Props
        {
            get { return new string[0]; }
        }

        protected BaseRepository(LocatorContext context)
        {
            Context = context;
        }

        public TEntity Add(TEntity entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public virtual TEntity Get(long entityId)
        {
            return GetDbSet.FirstOrDefault(x => x.ID == entityId);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var a = GetDbSet.ToArray();
            return a;
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return GetDbSet.Where(filter);
        }
    }
}
