namespace OnlineStore.CatalogService.Domain.Interfaces
{
    /// <summary>
    /// Generic repository interface.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets object of the entity by the uniquely identifier.
        /// </summary>
        /// <param name="id">The uniquely identifier of the entity.</param>
        /// <returns>The object of the entity.</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Gets objects list of the entities.
        /// </summary>
        /// <returns>List objects of the entities.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Inserts the object of the entity into storage.
        /// </summary>
        /// <param name="entity">The object of inserted entity.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Updates object of entity in storage.
        /// </summary>
        /// <param name="entity">The object of updated entity.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes object of entities in storage.
        /// </summary>
        /// <param name="entity">The object of deleted entity.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes array of objects.
        /// </summary>
        /// <param name="entities">Entities to delete.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task DeleteRangeAsync(TEntity[] entities);
    }
}
