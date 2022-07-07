namespace OnlineStore.CatalogService.Application.Common.Models.PaginationModels
{
    /// <summary>
    /// Paginated list model.
    /// </summary>
    /// <typeparam name="T">Type of items.</typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        /// <param name="items">Items.</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="totalCount">Total items count.</param>
        public PaginatedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            this.Items = items;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
        }

        /// <summary>
        /// Gets items.
        /// </summary>
        /// <value>
        /// <placeholder>Items.</placeholder>
        /// </value>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// Gets page number.
        /// </summary>
        /// <value>
        /// <placeholder>Page number.</placeholder>
        /// </value>
        public int PageNumber { get; }

        /// <summary>
        /// Gets page size.
        /// </summary>
        /// <value>
        /// <placeholder>Page size.</placeholder>
        /// </value>
        public int PageSize { get; }

        /// <summary>
        /// Gets total count.
        /// </summary>
        /// <value>
        /// <placeholder>Total count.</placeholder>
        /// </value>
        public int TotalCount { get; }

        /// <summary>
        /// Gets total pages.
        /// </summary>
        /// <value>
        /// <placeholder>Total pages.</placeholder>
        /// </value>
        public int TotalPages => this.PageSize == default ? 1 : (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);
    }
}
