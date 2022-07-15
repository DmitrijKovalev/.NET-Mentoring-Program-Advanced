using MediatR;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Products.Commands.AddProduct
{
    /// <summary>
    /// Add product command.
    /// </summary>
    public class AddProductCommand : IRequest<AddProductCommandResult>
    {
        /// <summary>
        /// Gets or sets product to add.
        /// </summary>
        /// <value>
        /// <placeholder>Product to add.</placeholder>
        /// </value>
        public ProductViewModel Product { get; set; }
    }
}
