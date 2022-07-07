using MediatR;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Updates product command.
    /// </summary>
    public class UpdateProductCommand : IRequest<CommandResponseModel>
    {
        /// <summary>
        /// Gets or sets product to update.
        /// </summary>
        /// <value>
        /// <placeholder>Product to update.</placeholder>
        /// </value>
        public ProductViewModel Product { get; set; }
    }
}
