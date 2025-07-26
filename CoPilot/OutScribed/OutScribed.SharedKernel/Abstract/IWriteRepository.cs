using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.SharedKernel.Abstract
{
    using System.Linq;
    using System.Threading.Tasks;

    namespace OutScribed.SharedKernel.Abstract
    {
        /// <summary>
        /// Generic repository interface for entities/aggregate roots.
        /// </summary>
        /// <typeparam name="T">The type of the entity/aggregate root, constrained to be an AggregateRoot.</typeparam>
        public interface IWriteRepository<T> where T : AggregateRoot // Assuming AggregateRoot is your base class for aggregates
        {
            /// <summary>
            /// Adds a new entity to the repository.
            /// </summary>
            /// <param name="entity">The entity to add.</param>
            /// <returns>A task that represents the asynchronous operation.</returns>
            Task AddAsync(T entity);

            /// <summary>
            /// Retrieves an entity by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the entity.</param>
            /// <returns>A task that represents the asynchronous operation, returning the entity if found, otherwise null.</returns>
            Task<T?> GetByIdAsync(Ulid id); // Assuming Ulid is your Id type for AggregateRoots

            /// <summary>
            /// Updates an existing entity in the repository.
            /// </summary>
            /// <param name="entity">The entity to update.</param>
            /// <returns>A task that represents the asynchronous operation.</returns>
            Task UpdateAsync(T entity);

            /// <summary>
            /// Deletes an entity from the repository.
            /// </summary>
            /// <param name="entity">The entity to delete.</param>
            /// <returns>A task that represents the asynchronous operation.</returns>
            Task DeleteAsync(T entity);

            /// <summary>
            /// Saves all changes made in this unit of work to the underlying data store.
            /// </summary>
            /// <returns>A task that represents the asynchronous save operation, returning the number of state entries written to the database.</returns>
            Task<int> SaveAsync();

            /// <summary>
            /// Returns an IQueryable for the entity type, allowing for complex queries.
            /// This should be used carefully to avoid leaking persistence concerns outside the infrastructure layer.
            /// </summary>
            /// <returns>An IQueryable of the entity type.</returns>
            IQueryable<T> GetQueryable();

            Task<int> CountAsync(ISpecification<T> spec);

            Task<List<T>> ListAsync(ISpecification<T> spec);

            Task<T?> FirstOrDefaultAsync(ISpecification<T> spec);

            Task<bool> AnyAsync(ISpecification<T> spec); // Useful for existence checks
        }
    }
}
