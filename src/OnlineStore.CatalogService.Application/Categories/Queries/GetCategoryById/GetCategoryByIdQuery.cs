using MediatR;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Categories.Queries.GetCategoryById
{
    /// <summary>
    /// Get category by id query model.
    /// </summary>
    public class GetCategoryByIdQuery : IRequest<CategoryViewModel>
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        /// <value>
        /// <placeholder>Category id.</placeholder>
        /// </value>
        public int CategoryId { get; set; }
    }
}
