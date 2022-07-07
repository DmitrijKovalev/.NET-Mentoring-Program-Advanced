using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineStore.CatalogService.WebApi.Configuration.Swagger
{
    /// <summary>
    /// Configure swagger options.
    /// </summary>
    public class ConfigureSwaggerOptions
        : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">API version description provider.</param>
        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Configure swagger options instance.
        /// </summary>
        /// <param name="options">Swagger options.</param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in this.provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        /// <summary>
        /// Configure swagger options instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configure.</param>
        public void Configure(string name, SwaggerGenOptions options)
        {
            this.Configure(options);
        }

        private static OpenApiInfo CreateVersionInfo(
                ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Cart Service API",
                Version = description.ApiVersion.ToString(),
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
