namespace Backend.Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Adds an entity to database
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to database
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Removes an entity based on its Id
        /// </summary>
        /// <param name="Id"></param>
        void Remove(int Id);

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Removes a range of entities
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);

    }

}
