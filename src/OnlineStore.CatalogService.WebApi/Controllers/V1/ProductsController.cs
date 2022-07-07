using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.Products.Commands.AddProduct;
using OnlineStore.CatalogService.Application.Products.Commands.DeleteProducts;
using OnlineStore.CatalogService.Application.Products.Commands.UpdateProduct;
using OnlineStore.CatalogService.Application.Products.Queries.GetProductsByCategoryIdWithPagination;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.WebApi.Controllers.V1
{
    /// <summary>
    /// The products controller.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
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
        /// <param name="categoryId">The category Id.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>List of products.</returns>
        [HttpGet]
        [Route("~/api/v1/categories/{categoryId}/[controller]")]
        public async Task<ActionResult<PaginatedList<ProductViewModel>>> GetProductsByCategoryIdAsync(
            [FromRoute] int categoryId,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var query = new GetProductsByCategoryIdWithPaginationQuery
            {
                CategoeyId = categoryId,
            };

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
