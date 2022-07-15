using MediatR;
using OnlineStore.CatalogService.Application.Common.Models;

namespace OnlineStore.CatalogService.Application.Categories.Commands.DeleteCategory
{
    /// <summary>
    /// Delete category command.
    /// </summary>
    public class DeleteCategoryCommand : IRequest<CommandResponseModel>
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        /// <value>
        /// <placeholder>Category id.</placeholder>
        /// </value>
        public int CategoeyId { get; set; }
    }
}
