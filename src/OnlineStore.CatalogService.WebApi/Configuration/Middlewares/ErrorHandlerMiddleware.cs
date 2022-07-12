using OnlineStore.CatalogService.Domain.Common.Exceptions;
using System.Net;
using System.Text.Json;
using IHostEnvironment = Microsoft.Extensions.Hosting.IHostEnvironment;

namespace OnlineStore.CatalogService.WebApi.Configuration.Middlewares
{
    /// <summary>
    /// Global error handler middleware.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">Request delegate.</param>
        /// <param name="environment">The hosting environment.</param>
        public ErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment environment)
        {
            this.next = next;
            this.environment = environment;
        }

        /// <summary>
        /// Invoke middleware action.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case CategoryNotFoundException:
                    case ProductNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var message = this.environment.IsDevelopment() ? error?.Message : "Internal Server Error.";
                var result = JsonSerializer.Serialize(new { message });
                await response.WriteAsync(result);
            }
        }
    }
}
