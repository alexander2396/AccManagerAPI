using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccManager.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public DbContext GetDbContext()
        {
            return Context;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public T Save(T entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public T Detach(T entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public T Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            return DbSet.Remove(entityToDelete).Entity;
        }

        public virtual T Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
    }
}
