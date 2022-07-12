using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OnlineStore.CartService.Core.Models.Configuration;

namespace OnlineStore.CartService.WebApi.Tests.Integration.Common
{
    internal class CartServiceWebApiFactory : WebApplicationFactory<Program>
    {
        private readonly Configuration configuration;

        public CartServiceWebApiFactory(Configuration configuration)
        {
            this.configuration = configuration;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(CartServiceConfiguration));
                services.AddSingleton(new CartServiceConfiguration { DatabaseConnectionString = this.configuration.DatabaseConnectionString });
            });

            return base.CreateHost(builder);
        }
    }
}
