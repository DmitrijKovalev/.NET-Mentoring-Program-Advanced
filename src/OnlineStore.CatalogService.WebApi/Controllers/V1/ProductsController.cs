using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.Products.Commands.AddProduct;
using OnlineStore.CatalogService.Application.Products.Commands.DeleteProducts;
using OnlineStore.CatalogService.Application.Products.Commands.UpdateProduct;
using OnlineStore.CatalogService.Application.Products.Queries.GetProductsByCategoryIdWithPagination;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.WebApi.Models;

namespace OnlineStore.CatalogService.WebApi.Controllers.V1
{
    /// <summary>
    /// The products controller.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get list of products by category Id.
        /// </summary>
        /// <param name="queryParameters">The query parameters.</param>
        /// <returns>List of products.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<ProductViewModel>>> GetProductsByCategoryIdAsync(
            [FromQuery] ProductsQueryParemeters queryParameters)
        {
            var query = new GetProductsByCategoryIdWithPaginationQuery
            {
                CategoeyId = queryParameters.CategoryId,
            };

            var pageNumber = queryParameters?.PageNumber;
            var pageSize = queryParameters?.PageSize;

            if (pageNumber is not null && pageSize is not null)
            {
                query.Pagination = new Pagination((int)pageNumber, (int)pageSize);
            }

            var response = await this.mediator.Send(query);

            return this.Ok(response);
        }

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <param name="product">Product to add.</param>
        /// <returns>Id of created product.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddProductCommandResult>> AddProductAsync([FromBody] ProductViewModel product)
        {
            var command = new AddProductCommand
            {
                Product = product,
            };

            var response = await this.mediator.Send(command);

            return this.Ok(response);
        }

        /// <summary>
        /// Update product.
        /// </summary>
        /// <param name="product">Product to update.</param>
        /// <returns>Result of updating.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommandResponseModel>> UpdateProductAsync([FromBody] ProductViewModel product)
        {
            var command = new UpdateProductCommand
            {
                Product = product,
            };

            var response = await this.mediator.Send(command);

            return this.Ok(response);
        }

        /// <summary>
        /// Delete product by Id.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <returns>Result of deleting.</returns>
        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommandResponseModel>> DeleteProductAsync([FromRoute] int productId)
        {
            var command = new DeleteProductCommand
            {
                ProductId = productId,
            };

            var result = await this.mediator.Send(command);

            return this.Ok(result);
        }
    }
}
