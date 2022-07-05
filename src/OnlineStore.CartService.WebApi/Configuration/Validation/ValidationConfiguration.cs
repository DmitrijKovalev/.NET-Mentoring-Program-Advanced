using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.CartService.WebApi.Configuration.Validation
{
    /// <summary>
    /// Models validation configuration.
    /// </summary>
    public static class ValidationConfiguration
    {
        /// <summary>
        /// Add models validation.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns>The collection of service descriptors.</returns>
        public static IServiceCollection AddModelsValidation(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
