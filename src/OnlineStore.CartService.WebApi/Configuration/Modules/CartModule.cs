using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models.Configuration;
using OnlineStore.CartService.DataAccessLayer;

namespace OnlineStore.CartService.WebApi.Configuration.Modules
{
    /// <summary>
    /// Cart module.
    /// </summary>
    public static class CartModule
    {
        /// <summary>
        /// Add cart service module.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="appSettings">Application settings.</param>
        /// <returns>The collection of service descriptors.</returns>
        public static IServiceCollection AddCartServiceModule(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddSingleton(new CartServiceConfiguration { DatabaseConnectionString = appSettings.DatabaseConnectionString });
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, BusinessLogicLayer.CartService>();

            return services;
        }
    }
}
