using OnlineStore.CartService.WebApi.Configuration;
using OnlineStore.CartService.WebApi.Configuration.Middlewares;
using OnlineStore.CartService.WebApi.Configuration.Modules;
using OnlineStore.CartService.WebApi.Configuration.Swagger;
using OnlineStore.CartService.WebApi.Configuration.Validation;
using OnlineStore.CartService.WebApi.Configuration.Versioning;

var builder = WebApplication.CreateBuilder(args);
{
    var settings = new AppSettings();
    builder.Configuration.Bind(settings);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerDocumentation();
    builder.Services.AddVersioning();
    builder.Services.AddModelsValidation();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddCartServiceModule(settings);
}

var app = builder.Build();
{
    app.UseSwaggerDocumentation();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseGlobalErrorHandler();
    app.Run();
}
