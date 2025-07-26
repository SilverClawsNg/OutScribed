using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Abstract;

namespace OutScribed.Infrastructure.Persistence.Writes.Repository
{
    /// <summary>
    /// Generic repository interface for entities/aggregate roots.
    /// </summary>
    /// <typeparam name="T">The type of the entity/aggregate root, constrained to be an AggregateRoot.</typeparam>
    /// Generic repository interface for entities/aggregate roots.
    /// </summary>
    /// <typeparam name="T">The type of the entity/aggregate root, constrained to be an AggregateRoot.</typeparam>
    public class WriteRepository<T, TDbContext> : IWriteRepository<T>
        where T : AggregateRoot
        where TDbContext : DbContext
    {
        protected readonly TDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public WriteRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(Ulid id)
        {
            // Use FindAsync for primary key lookup, which might use change tracker first
            return await _dbSet.FindAsync(id);
            // Alternatively, for more control over eager loading:
            // return await _dbSet.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(T entity)
        {
            // EF Core tracks changes for attached entities automatically.
            // If the entity is detached, you might explicitly attach it or mark its state.
            _dbSet.Update(entity);
            return Task.CompletedTask; // Or return await Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            // Apply only the criteria part of the specification to get the count
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, spec).CountAsync();
        }

        // New Specification methods
        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, spec).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, spec).FirstOrDefaultAsync();
        }

        public async Task<bool> AnyAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, spec).AnyAsync();
        }
    }
}