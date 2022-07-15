using MediatR;
using OnlineStore.CatalogService.Application.Common.Models;

namespace OnlineStore.CatalogService.Application.Products.Commands.DeleteProducts
{
    /// <summary>
    /// Delete product command.
    /// </summary>
    public class DeleteProductCommand : IRequest<CommandResponseModel>
    {
        /// <summary>
        /// Gets or sets product id.
        /// </summary>
        /// <value>
        /// <placeholder>Product id.</placeholder>
        /// </value>
        public int ProductId { get; set; }
    }
}
