using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WebApiXmlCommentsFile = OnlineStore.CatalogService.WebApi.Configuration.Swagger.XmlCommentsFile;
using ApplicationXmlCommentsFile = OnlineStore.CatalogService.Application.Common.Configuration.XmlCommentsFile;

namespace OnlineStore.CatalogService.WebApi.Configuration.Swagger
{
    /// <summary>
    /// The Swagger configuration.
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Add swagger documentation.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns>The collection of service descriptors.</returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.IncludeXmlComments(WebApiXmlCommentsFile.FullFilePath);
                options.IncludeXmlComments(ApplicationXmlCommentsFile.FullFilePath);
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            return services;
        }

        /// <summary>
        /// Use swagger documentation.
        /// </summary>
        /// <param name="app">The web application used to configure the HTTP pipeline, and routes.</param>
        /// <returns>The web application.</returns>
        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            return app;
        }
    }
}
