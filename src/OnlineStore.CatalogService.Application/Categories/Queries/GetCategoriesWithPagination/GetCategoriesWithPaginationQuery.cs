using MediatR;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Categories.Queries.GetCategoriesWithPagination
{
    /// <summary>
    /// Get list of categories query model.
    /// </summary>
    public class GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryViewModel>>
    {
        /// <summary>
        /// Gets or sets pagination.
        /// </summary>
        /// <value>
        /// <placeholder>Pagination.</placeholder>
        /// </value>
        public Pagination Pagination { get; set; }
    }
}
