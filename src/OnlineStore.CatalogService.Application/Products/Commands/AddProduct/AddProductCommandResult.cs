using OnlineStore.CatalogService.Application.Common.Models;

namespace OnlineStore.CatalogService.Application.Products.Commands.AddProduct
{
    /// <summary>
    /// Add product command result.
    /// </summary>
    public class AddProductCommandResult : CommandResponseModel
    {
        /// <summary>
        /// Gets or sets added product id.
        /// </summary>
        /// <value>
        /// <placeholder>Added product id.</placeholder>
        /// </value>
        public int ProductId { get; set; }
    }
}
