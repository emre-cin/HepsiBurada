using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiBurada.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region Methods
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int SaveChanges(bool ensureTransaction = false); 
        #endregion
    }
}
