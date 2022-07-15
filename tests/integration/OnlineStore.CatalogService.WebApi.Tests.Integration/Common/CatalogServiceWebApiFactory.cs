using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OnlineStore.CatalogService.Infrastructure.Persistence.Interfaces;

namespace OnlineStore.CatalogService.WebApi.Tests.Integration.Common
{
    internal class CatalogServiceWebApiFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(IAppDataBaseFactory));
                services.AddSingleton<IAppDataBaseFactory, AppTestDataBaseFactory>();
            });

            return base.CreateHost(builder);
        }
    }
}
