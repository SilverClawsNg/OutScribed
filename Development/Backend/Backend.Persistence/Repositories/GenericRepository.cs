using Backend.Application.Repositories;
using Backend.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domain.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        #region Initializer

        private readonly BackendDbContext _dbContext;

        public GenericRepository(BackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public void Remove(int Id)
        {
            TEntity entity = _dbContext.Set<TEntity>().Find(Id);

            Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<TEntity>().Attach(entity);
            }

            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<TEntity>().Attach(entity);
            }
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        #endregion

    }

}
