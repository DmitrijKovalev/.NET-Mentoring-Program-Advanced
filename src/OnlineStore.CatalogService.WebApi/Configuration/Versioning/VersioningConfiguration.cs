using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.CatalogService.WebApi.Configuration.Versioning
{
    /// <summary>
    /// The versioning configuration.
    /// </summary>
    public static class VersioningConfiguration
    {
        /// <summary>
        /// Add versioning.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns>The collection of service descriptors.</returns>
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
