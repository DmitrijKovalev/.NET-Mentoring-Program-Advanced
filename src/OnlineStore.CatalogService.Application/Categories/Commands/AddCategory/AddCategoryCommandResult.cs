using OnlineStore.CatalogService.Application.Common.Models;

namespace OnlineStore.CatalogService.Application.Categories.Commands.AddCategory
{
    /// <summary>
    /// Add category command result.
    /// </summary>
    public class AddCategoryCommandResult : CommandResponseModel
    {
        /// <summary>
        /// Gets or sets added category id.
        /// </summary>
        /// <value>
        /// <placeholder>Added category id.</placeholder>
        /// </value>
        public int CategoryId { get; set; }
    }
}
