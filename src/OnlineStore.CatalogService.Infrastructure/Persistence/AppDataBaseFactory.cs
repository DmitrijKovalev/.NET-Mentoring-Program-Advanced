using Microsoft.EntityFrameworkCore;
using OnlineStore.CatalogService.Infrastructure.Persistence.Interfaces;

namespace OnlineStore.CatalogService.Infrastructure.Persistence
{
    /// <summary>
    /// Application database factory.
    /// </summary>
    public class AppDataBaseFactory : IAppDataBaseFactory
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDataBaseFactory"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public AppDataBaseFactory(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this.connectionString = connectionString;
        }

        /// <inheritdoc/>
        public AppDataBaseContext CreateNewInstance()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDataBaseContext>()
                .UseSqlServer(this.connectionString);

            return new AppDataBaseContext(optionsBuilder.Options);
        }
    }
}
