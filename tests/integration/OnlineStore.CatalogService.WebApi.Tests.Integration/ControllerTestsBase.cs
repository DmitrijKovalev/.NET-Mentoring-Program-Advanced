using OnlineStore.CatalogService.Infrastructure.Persistence;
using OnlineStore.CatalogService.WebApi.Tests.Integration.Common;

namespace OnlineStore.CatalogService.WebApi.Tests.Integration
{
    public class ControllerTestsBase : IDisposable
    {
        private readonly AppDataBaseContext appDataBaseContext;

        private bool disposed = false;

        public ControllerTestsBase()
        {
            this.appDataBaseContext = new AppTestDataBaseFactory().CreateNewInstance();
            this.HttpClient = new CatalogServiceWebApiFactory().CreateClient();
        }

        protected HttpClient HttpClient { get; set; }

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
                    this.appDataBaseContext.Database.EnsureDeleted();
                    this.appDataBaseContext.Dispose();

                    this.HttpClient.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
