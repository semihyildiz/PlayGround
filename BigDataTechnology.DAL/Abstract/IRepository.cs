using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BigDataTechnology.DAL.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Get(object Id);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
        TEntity Delete(object ID);
        IQueryable<TEntity> GetAllQueryable();
        bool DeleteWhere(Expression<Func<TEntity, bool>> predicate = null);
        int GetNewID(Func<TEntity, int> columnSelector);
        int TotalCount();
        int TotalCount(Expression<Func<TEntity, bool>> predicate = null);
    }
}
