using System;
using System.Threading.Tasks;
using AccManager.DataAccess.Repository;

namespace AccManager.DataAccess.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task SaveAsync();
        Task BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
