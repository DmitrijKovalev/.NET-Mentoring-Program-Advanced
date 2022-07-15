using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CatalogService.Application.Categories.Queries.GetCategoriesWithPagination;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.WebApi.Models.Hateoas;

namespace OnlineStore.CatalogService.WebApi.Controllers.V2
{
    /// <summary>
    /// The category controller.
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly LinkGenerator linkGeneratorr;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="linkGeneratorr">The link generator.</param>
        public CategoriesController(IMediator mediator, LinkGenerator linkGeneratorr)
        {
            this.mediator = mediator;
            this.linkGeneratorr = linkGeneratorr;
        }

        /// <summary>
        /// Get list of categories.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>List of categories.</returns>
        [HttpGet(Name = nameof(GetCategories))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LinkedCollectionResourceWrapper<CategoryViewModel>>> GetCategories(
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var query = new GetCategoriesWithPaginationQuery();

            if (pageNumber is not null && pageSize is not null)
            {
                query.Pagination = new Pagination((int)pageNumber, (int)pageSize);
            }

            var response = await this.mediator.Send(query);

            var result = this.CreateResourceLinks(response);

            return this.Ok(result);
        }

        private LinkedCollectionResourceWrapper<CategoryViewModel> CreateResourceLinks(PaginatedList<CategoryViewModel> resource)
        {
            var result = new LinkedCollectionResourceWrapper<CategoryViewModel>(resource.Items);

            if (resource.HasPreviousPage)
            {
                var urlPreviousPage = this.linkGeneratorr.GetUriByAction(this.HttpContext, nameof(this.GetCategories), values: new
                {
                    pageNumber = resource.PageNumber - 1,
                    pageSize = resource.PageSize,
                });

                var linkPreviousPage = new Link
                {
                    Href = urlPreviousPage,
                    Rel = ResourceUrlType.PreviousPage.ToString(),
                    Method = HttpMethod.Get.ToString(),
                };

                result.Links.Add(linkPreviousPage);
            }

            if (resource.HasNextPage)
            {
                var urlNextPage = this.linkGeneratorr.GetUriByAction(this.HttpContext, nameof(this.GetCategories), values: new
                {
                    pageNumber = resource.PageNumber + 1,
                    pageSize = resource.PageSize,
                });

                var linkNextPage = new Link
                {
                    Href = urlNextPage,
                    Rel = ResourceUrlType.NextPage.ToString(),
                    Method = HttpMethod.Get.ToString(),
                };

                result.Links.Add(linkNextPage);
            }

            return result;
        }
    }
}
