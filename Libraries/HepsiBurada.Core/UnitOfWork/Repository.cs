using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HepsiBurada.Core.UnitOfWork
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        #endregion

        #region Ctor
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        #endregion

        #region Methods
        public void Delete(int id)
        {
            Delete(GetById(id));
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void Insert(params TEntity[] entities) => _dbSet.AddRange(entities);

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(params TEntity[] entities) => _dbSet.UpdateRange(entities); 
        #endregion
    }
}
