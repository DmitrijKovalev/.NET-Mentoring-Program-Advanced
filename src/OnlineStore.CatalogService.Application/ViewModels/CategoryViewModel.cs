namespace OnlineStore.CatalogService.Application.ViewModels
{
    /// <summary>
    /// The category.
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        /// <value>
        /// <placeholder>Id.</placeholder>
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets category name.
        /// </summary>
        /// <value>
        /// <placeholder>Category name.</placeholder>
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets image URL.
        /// </summary>
        /// <value>
        /// <placeholder>Image URL.</placeholder>
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets parent category id.
        /// </summary>
        /// <value>
        /// <placeholder>Parent category id.</placeholder>
        /// </value>
        public int? ParentCategoryId { get; set; }
    }
}
