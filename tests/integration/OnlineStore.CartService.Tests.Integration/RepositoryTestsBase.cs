using OnlineStore.CartService.Core.Models.Configuration;
using OnlineStore.CartService.Tests.Integration.Common;
using OnlineStore.CartService.Tests.Integration.TestsFixture;

namespace OnlineStore.CartService.Tests.Integration
{
    public class RepositoryTestsBase : IDisposable
    {
        private bool disposed = false;

        public RepositoryTestsBase(Fixture mongoDatabaseFixture)
        {
            this.Configuration = new CartServiceConfiguration
            {
                DatabaseConnectionString = mongoDatabaseFixture.Configuration.DatabaseConnectionString,
            };

            this.MongoHelperRepository = new MongoHelperRepository(this.Configuration);
        }

        protected CartServiceConfiguration Configuration { get; set; }

        protected MongoHelperRepository MongoHelperRepository { get; set; }

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
                    this.MongoHelperRepository.GetContext().DropCollection(this.MongoHelperRepository.CartCollectionName);
                }
            }

            this.disposed = true;
        }
    }
}
