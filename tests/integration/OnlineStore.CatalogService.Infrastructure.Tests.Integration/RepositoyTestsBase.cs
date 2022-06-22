using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Dac;
using OnlineStore.CatalogService.Infrastructure.Tests.Integration.TestsFixture;
using OnlineStore.CatalogService.Infrastructure.Persistence;

namespace OnlineStore.CatalogService.Infrastructure.Tests.Integration
{
    public class RepositoyTestsBase : IDisposable
    {
        private const string DacPacPath = @"src\OnlineStore.CatalogService.Database\Snapshots\OnlineStore.CatalogService.Database.dacpac";

        private bool disposed = false;

        public RepositoyTestsBase(Fixture fixture)
        {
            this.DataBaseFactory = new AppDataBaseFactory(fixture.Configuration.DatabaseConnectionString);
            this.PublishDatabase(fixture.Configuration.DatabaseConnectionString);
        }

        protected AppDataBaseFactory DataBaseFactory { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.DataBaseFactory.CreateNewInstance().Database.EnsureDeleted();
                }
            }

            this.disposed = true;
        }

        private void PublishDatabase(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
            var dacpac = DacPackage.Load(@$"{solutiondir}\{DacPacPath}");
            var dacpacService = new DacServices(connectionString);
            dacpacService.Publish(dacpac, connectionStringBuilder.InitialCatalog, new PublishOptions());
        }
    }
}
