using OnlineStore.CatalogService.Application.Common.Configuration;
using OnlineStore.CatalogService.WebApi.Configuration.Middlewares;
using OnlineStore.CatalogService.WebApi.Configuration.Swagger;
using OnlineStore.CatalogService.WebApi.Configuration.Versioning;

var builder = WebApplication.CreateBuilder(args);
{
    var settings = new AppSettings();
    builder.Configuration.Bind(settings);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerDocumentation();
    builder.Services.AddVersioning();
    builder.Services.AddApplicationServices(settings);
}

var app = builder.Build();
{
    app.UseSwaggerDocumentation();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseGlobalErrorHandler();
    app.Run();
}
