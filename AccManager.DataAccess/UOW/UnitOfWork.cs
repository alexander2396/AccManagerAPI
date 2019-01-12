using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using AccManager.DataAccess.EF.Context;
using AccManager.DataAccess.Repository;

namespace AccManager.DataAccess.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        protected Dictionary<Type, object> Repositories = new Dictionary<Type, object>();
        private static readonly object CreateRepositoryLock = new object();

        private IDbContextTransaction _transaction;

        private bool _disposed;
        private bool _transactionClosed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (!Repositories.ContainsKey(typeof(TEntity)))
            {
                lock (CreateRepositoryLock)
                {
                    if (!Repositories.ContainsKey(typeof(TEntity)))
                    {
                        CreateRepository<TEntity>();
                    }
                }
            }

            return Repositories[typeof(TEntity)] as IRepository<TEntity>;
        }

        private void CreateRepository<TEntity>() where TEntity : class
        {
            Repositories.Add(typeof(TEntity), new Repository<TEntity>(_context));
        }

        #region IUnitOfWork

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            _transactionClosed = false;
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transactionClosed = true;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transactionClosed = true;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (!_disposed) Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null && !_transactionClosed)
                {
                    RollbackTransaction();
                }
                _context?.Dispose();
                _disposed = true;
            }
        }

        #endregion
    }
}
