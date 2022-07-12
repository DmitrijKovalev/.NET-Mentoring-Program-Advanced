using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CatalogService.Application.Categories.Commands.AddCategory;
using OnlineStore.CatalogService.Application.Categories.Commands.DeleteCategory;
using OnlineStore.CatalogService.Application.Categories.Commands.UpdateCategory;
using OnlineStore.CatalogService.Application.Categories.Queries.GetCategoriesWithPagination;
using OnlineStore.CatalogService.Application.Categories.Queries.GetCategoryById;
using OnlineStore.CatalogService.Application.Common.Models;
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
        /// Get list of categories.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>List of categories.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<CategoryViewModel>>> GetCategoriesAsync(
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

        /// <summary>
        /// Get category by Id.
        /// </summary>
        /// <param name="categoryId">Category Id.</param>
        /// <returns>Category.</returns>
        [HttpGet]
        [Route("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryAsync([FromRoute] int categoryId)
        {
            var query = new GetCategoryByIdQuery
            {
                CategoryId = categoryId,
            };

            var response = await this.mediator.Send(query);

            return this.Ok(response);
        }

        /// <summary>
        /// Add new category.
        /// </summary>
        /// <param name="category">Category to add.</param>
        /// <returns>Id of created category.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddCategoryCommandResult>> AddCategoryAsync([FromBody] CategoryViewModel category)
        {
            var command = new AddCategoryCommand
            {
                Category = category,
            };

            var response = await this.mediator.Send(command);

            return this.Ok(response);
        }

        /// <summary>
        /// Update category.
        /// </summary>
        /// <param name="category">Category to update.</param>
        /// <returns>Result of updating.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommandResponseModel>> UpdateCategoryAsync([FromBody] CategoryViewModel category)
        {
            var command = new UpdateCategoryCommand
            {
                Category = category,
            };

            var response = await this.mediator.Send(command);

            return this.Ok(response);
        }

        /// <summary>
        /// Delete category by Id.
        /// </summary>
        /// <param name="categoryId">Category Id.</param>
        /// <returns>Result of deleting.</returns>
        [HttpDelete]
        [Route("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommandResponseModel>> DeleteCategoryAsync([FromRoute] int categoryId)
        {
            var command = new DeleteCategoryCommand
            {
                CategoeyId = categoryId,
            };

            var result = await this.mediator.Send(command);

            return this.Ok(result);
        }
    }
}
