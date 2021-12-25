using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HepsiBurada.Core.UnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Methods
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true);
        TEntity GetById(int id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         bool disableTracking = true);
        TEntity Insert(TEntity entity);

        void Insert(params TEntity[] entities);

        void Update(TEntity entity);

        void Update(params TEntity[] entities);

        void Delete(int id);

        void Delete(TEntity entity);

        void Delete(params TEntity[] entities); 
        #endregion
    }
}
