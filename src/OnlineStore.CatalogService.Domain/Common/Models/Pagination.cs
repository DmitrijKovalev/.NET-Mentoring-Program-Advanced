namespace OnlineStore.CatalogService.Domain.Common.Models
{
    /// <summary>
    /// Pagination model.
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pagination"/> class.
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Count of items per page.</param>
        public Pagination(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// Gets or sets page number.
        /// </summary>
        /// <value>
        /// <placeholder>Page number.</placeholder>
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets page size.
        /// </summary>
        /// <value>
        /// <placeholder>Page size.</placeholder>
        /// </value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets count items to skip.
        /// </summary>
        /// <value>
        /// <placeholder>Count items to skip.</placeholder>
        /// </value>
        public int SkipCount => this.PageNumber * this.PageSize;
    }
}
