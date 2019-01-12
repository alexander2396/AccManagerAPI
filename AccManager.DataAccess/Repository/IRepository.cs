using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccManager.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        DbContext GetDbContext();
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetQueryable();
        T Save(T entity);
        T Detach(T entity);
        T Delete(T entityToDelete);
        T Update(T entityToUpdate);
    }
}
