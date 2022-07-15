using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineStore.CatalogService.Infrastructure.Persistence;
using OnlineStore.CatalogService.Infrastructure.Persistence.Interfaces;

namespace OnlineStore.CatalogService.WebApi.Tests.Integration.Common
{
    internal class AppTestDataBaseFactory : IAppDataBaseFactory
    {
        private InMemoryDatabaseRoot inMemoryDatabaseRoot;

        private InMemoryDatabaseRoot Root
        {
            get
            {
                if (this.inMemoryDatabaseRoot == null)
                {
                    this.inMemoryDatabaseRoot = new InMemoryDatabaseRoot();
                }

                return this.inMemoryDatabaseRoot;
            }
        }

        /// <inheritdoc/>
        public AppDataBaseContext CreateNewInstance()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDataBaseContext>()
                .UseInMemoryDatabase("Testing", this.Root);

            return new AppDataBaseContext(optionsBuilder.Options);
        }
    }
}
