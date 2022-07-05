namespace OnlineStore.CartService.WebApi.Configuration.Middlewares
{
    /// <summary>
    /// Middleware extensions.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Use global error handler.
        /// </summary>
        /// <param name="app">The web application used to configure the HTTP pipeline, and routes.</param>
        /// <returns>The web application.</returns>
        public static IApplicationBuilder UseGlobalErrorHandler(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }
    }
}
