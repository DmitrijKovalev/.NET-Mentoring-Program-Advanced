using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineStore.CatalogService.WebApi.Models
{
    /// <summary>
    /// Products query parameters.
    /// </summary>
    public class ProductsQueryParemeters
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        /// <value>
        /// <placeholder>Category id.</placeholder>
        /// </value>
        [BindRequired]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets page number.
        /// </summary>
        /// <value>
        /// <placeholder>Page number.</placeholder>
        /// </value>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Gets or sets page size.
        /// </summary>
        /// <value>
        /// <placeholder>Page size.</placeholder>
        /// </value>
        public int? PageSize { get; set; }
    }
}
