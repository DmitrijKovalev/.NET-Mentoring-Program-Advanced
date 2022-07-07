using Microsoft.EntityFrameworkCore;
using OnlineStore.CatalogService.Domain.Interfaces;
using OnlineStore.CatalogService.Infrastructure.Persistence.Interfaces;

namespace OnlineStore.CatalogService.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class EfRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly DbContext context;

        private readonly DbSet<TEntity> dbset;

        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="factory">Application database factory.</param>
        public EfRepository(IAppDataBaseFactory factory)
        {
            this.context = factory.CreateNewInstance();
            this.dbset = this.context.Set<TEntity>();
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await this.dbset.FindAsync(id);
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> GetAll()
        {
            return this.dbset.AsNoTracking();
        }

        /// <inheritdoc/>
        public async Task InsertAsync(TEntity entity)
        {
            await this.dbset.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TEntity entity)
        {
            this.dbset.Update(entity);
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(TEntity entity)
        {
            this.dbset.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteRangeAsync(TEntity[] entity)
        {
            this.dbset.RemoveRange(entity);
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Releases all resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// True to release both managed and unmanaged resources;
        /// False to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
