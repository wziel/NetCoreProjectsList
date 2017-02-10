using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreProjectsList.Repositories
{
    public abstract class BaseRepository<T, TId> : IBaseRepository<T, TId> where T : class
    {
        protected DbSet<T> DbSet { get; private set; }
        protected DbContext DbContext { get; set; }

        public BaseRepository(DbSet<T> dbSet, DbContext dbContext)
        {
            DbSet = dbSet;
            DbContext = dbContext;
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get(TId id)
        {
            return DbSet.SingleOrDefault(GetByIdPredicate(id));
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }

        public void Delete(TId id)
        {
            var entites = DbSet.Where(GetByIdPredicate(id));
            foreach(var entity in entites)
            {
                DbSet.Remove(entity);
            }
            DbContext.SaveChanges();
        }

        public void Edit(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
        public abstract Expression<Func<T, bool>> GetByIdPredicate(TId id);
    }

    public interface IBaseRepository<T, TId> where T : class
    {
        List<T> GetAll();
        T Get(TId id);
        void Add(T entity);
        void Delete(T entity);
        void Delete(TId id);
        void Edit(T entity);
    }
}
