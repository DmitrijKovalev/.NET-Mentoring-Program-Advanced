using Microsoft.EntityFrameworkCore;
using OnlineStore.CatalogService.Infrastructure.Persistence.Interfaces;

namespace OnlineStore.CatalogService.Infrastructure.Persistence
{
    /// <summary>
    /// Application database factory.
    /// </summary>
    public class AppDataBaseFactory : IAppDataBaseFactory
    {
        private readonly AppDataBaseConnectionConfiguration connectionConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDataBaseFactory"/> class.
        /// </summary>
        /// <param name="connectionConfiguration">Connection configuration.</param>
        public AppDataBaseFactory(AppDataBaseConnectionConfiguration connectionConfiguration)
        {
            if (connectionConfiguration is null)
            {
                throw new ArgumentNullException(nameof(connectionConfiguration));
            }

            this.connectionConfiguration = connectionConfiguration;
        }

        /// <inheritdoc/>
        public AppDataBaseContext CreateNewInstance()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDataBaseContext>()
                .UseSqlServer(this.connectionConfiguration.ConnectionString);

            return new AppDataBaseContext(optionsBuilder.Options);
        }
    }
}
