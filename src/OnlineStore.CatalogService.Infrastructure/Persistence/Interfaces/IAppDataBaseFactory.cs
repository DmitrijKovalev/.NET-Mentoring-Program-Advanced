namespace OnlineStore.CatalogService.Infrastructure.Persistence.Interfaces
{
    /// <summary>
    /// Application database factory interface.
    /// </summary>
    public interface IAppDataBaseFactory
    {
        /// <summary>
        /// Create a new instance of database context.
        /// </summary>
        /// <returns>Database context.</returns>
        public AppDataBaseContext CreateNewInstance();
    }
}
