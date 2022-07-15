using OnlineStore.CartService.Core.Models.Configuration;
using OnlineStore.CartService.DataAccessLayer;
using OnlineStore.CartService.WebApi.Tests.Integration.TestsFixture;

namespace OnlineStore.CartService.WebApi.Tests.Integration
{
    public class CartControllerTestsBase : IDisposable
    {
        private bool disposed = false;

        public CartControllerTestsBase(Fixture fixture)
        {
            var configuration = new CartServiceConfiguration
            {
                DatabaseConnectionString = fixture.Configuration.DatabaseConnectionString,
            };

            this.Repository = new BaseRepository(configuration);
            this.HttpClient = fixture.HttpClient;
        }

        protected HttpClient HttpClient { get; set; }

        private BaseRepository Repository { get; set; }

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
                    this.Repository.GetContext().DropCollection(this.Repository.CartCollectionName);
                }
            }

            this.disposed = true;
        }
    }
}
