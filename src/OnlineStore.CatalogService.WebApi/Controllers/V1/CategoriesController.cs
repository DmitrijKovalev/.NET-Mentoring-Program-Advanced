using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CatalogService.Application.Categories.Queries.GetCategoriesWithPagination;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.WebApi.Controllers.V1
{
    /// <summary>
    /// The category controller.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get list categories.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>Name.</returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<CategoryViewModel>>> GetCategories(
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var query = new GetCategoriesWithPaginationQuery();

            if (pageNumber is not null && pageSize is not null)
            {
                query.Pagination = new Pagination((int)pageNumber, (int)pageSize);
            }

            var response = await this.mediator.Send(query);

            return this.Ok(response);
        }
    }
}
