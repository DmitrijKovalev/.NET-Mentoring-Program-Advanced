using MediatR;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Categories.Commands.AddCategory
{
    /// <summary>
    /// Add category command.
    /// </summary>
    public class AddCategoryCommand : IRequest<AddCategoryCommandResult>
    {
        /// <summary>
        /// Gets or sets category to add.
        /// </summary>
        /// <value>
        /// <placeholder>Category to add.</placeholder>
        /// </value>
        public CategoryViewModel Category { get; set; }
    }
}
