using MediatR;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Products.Queries.GetProductsByCategoryIdWithPagination
{
    /// <summary>
    /// Get products by category with pagination query.
    /// </summary>
    public class GetProductsByCategoryIdWithPaginationQuery : IRequest<PaginatedList<ProductViewModel>>
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        /// <value>
        /// <placeholder>Category id.</placeholder>
        /// </value>
        public int CategoeyId { get; set; }

        /// <summary>
        /// Gets or sets pagination.
        /// </summary>
        /// <value>
        /// <placeholder>Pagination.</placeholder>
        /// </value>
        public Pagination Pagination { get; set; }
    }
}
