using POSSystemWithInventory.Data;
using POSSystemWithInventory.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext dbContext;
        public BaseRepository(AppDbContext databaseConnection)
        {
            dbContext = databaseConnection;
        }
        // Implementing IBaseRepository

        #region Searching
        public TEntity Get(int Id)
        {
            return dbContext.Set<TEntity>().Find(Id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate);
        }
        #endregion

        #region Saving
        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }
        public void AddAll(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().AddRange(entities);
        }
        #endregion

        #region Deleting
        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }
        public void RemoveAll(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
        }
        #endregion
    }
}
