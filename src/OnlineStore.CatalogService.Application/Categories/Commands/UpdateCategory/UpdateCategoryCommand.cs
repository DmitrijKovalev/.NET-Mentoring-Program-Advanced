using MediatR;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Categories.Commands.UpdateCategory
{
    /// <summary>
    /// Updates category command.
    /// </summary>
    public class UpdateCategoryCommand : IRequest<CommandResponseModel>
    {
        /// <summary>
        /// Gets or sets category to update.
        /// </summary>
        /// <value>
        /// <placeholder>Category to update.</placeholder>
        /// </value>
        public CategoryViewModel Category { get; set; }
    }
}
