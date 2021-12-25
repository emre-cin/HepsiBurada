using HepsiBurada.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiBurada.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly HepsiBuradaContext _context;
        private bool _disposed = false;
        private readonly IDbContextTransaction _transaction;
        #endregion

        #region Ctor
        public UnitOfWork(HepsiBuradaContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public int SaveChanges(bool ensureTransaction = false)
        {
            if (ensureTransaction == false)
                return _context.SaveChanges();

            var transaction = _transaction ?? _context.Database.BeginTransaction();

            using (transaction)
            {
                try
                {
                    if (_context == null)
                        throw new ArgumentException("Context is null");

                    int result = _context.SaveChanges();

                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error on save changes ", ex);
                }
            }
        } 
        #endregion
    }
}
