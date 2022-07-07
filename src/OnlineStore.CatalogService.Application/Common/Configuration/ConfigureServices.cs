using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;
using OnlineStore.CatalogService.Domain.Services;
using OnlineStore.CatalogService.Infrastructure.Persistence;
using OnlineStore.CatalogService.Infrastructure.Persistence.Interfaces;
using System.Reflection;

namespace OnlineStore.CatalogService.Application.Common.Configuration
{
    /// <summary>
    /// Configuration of application Services.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Add application services.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="appSettings">Application settings.</param>
        /// <returns>The collection of service descriptors.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton(new AppDataBaseConnectionConfiguration { ConnectionString = appSettings.DatabaseConnectionString });

            services.AddScoped<IAppDataBaseFactory, AppDataBaseFactory>();

            services.AddScoped<IRepository<Category>, EfRepository<Category>>();
            services.AddScoped<IRepository<Product>, EfRepository<Product>>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
