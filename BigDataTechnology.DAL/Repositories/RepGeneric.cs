using BigDataTechnology.DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BigDataTechnology.DAL.Repositories
{
    public class RepGeneric<Context, TEntity> : IRepository<TEntity> where TEntity : class where Context : DbContext
    {
        private readonly Context _dbContext;
        private DbSet<TEntity> _dbSet;
        public RepGeneric(Context dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        protected Context DbContext
        {
            get { return _dbContext; }
        }
        public virtual TEntity Add(TEntity entity)
        {
            if (null == entity)
            {
                return entity;
            }
            DbContext.Set<TEntity>().Add(entity);

            return entity;
        }
        public virtual TEntity Update(TEntity entity)
        {
            if (null == entity)
            {
                return entity;
            }
            _dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public virtual TEntity Delete(TEntity entity)
        {
            if (null == entity)
            {
                return entity;
            }
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return entity;
        }
        public virtual TEntity Delete(object ID)
        {
            return Delete(DbContext.Set<TEntity>().Find(ID));
        }
        public TEntity Get(object Id)
        {
            return DbContext.Set<TEntity>().Find(Id);
        }
        public List<TEntity> GetAll()
        {

            return DbContext.Set<TEntity>().ToList();
        }
        public IQueryable<TEntity> GetAllQueryable()
        {
            return DbContext.Set<TEntity>();
        }
        public bool DeleteWhere(Expression<Func<TEntity, bool>> predicate = null)
        {
            var dbSet = DbContext.Set<TEntity>();
            if (predicate != null)
                dbSet.RemoveRange(dbSet.Where(predicate));
            else
                dbSet.RemoveRange(dbSet);

            return true;
        }
        public int GetNewID(Func<TEntity, int> columnSelector)
        {
            int LastCode = 0;
            try
            {
                LastCode = DbContext.Set<TEntity>().Max(columnSelector);
            }
            catch (Exception) { }

            return LastCode + 1;
        }

        public int TotalCount()
        {
            return DbContext.Set<TEntity>().Count();
        }

        public int TotalCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            var dbSet = DbContext.Set<TEntity>();
            if (predicate != null)
                return dbSet.Where(predicate).Count();
            else
                return dbSet.Count();
        }
    }
}
