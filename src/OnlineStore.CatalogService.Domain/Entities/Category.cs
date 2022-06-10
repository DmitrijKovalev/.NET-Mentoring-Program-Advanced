namespace OnlineStore.CatalogService.Domain.Entities
{
    /// <summary>
    /// The category.
    /// </summary>
    public class Category
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

        /// <summary>
        /// Gets or sets parent category.
        /// </summary>
        /// <value>
        /// <placeholder>Parent category.</placeholder>
        /// </value>
        public Category ParentCategory { get; set; }

        /// <summary>
        /// Gets or sets child categories.
        /// </summary>
        /// <value>
        /// <placeholder>Child categories.</placeholder>
        /// </value>
        public IEnumerable<Category> ChildCategories { get; set; }

        /// <summary>
        /// Gets or sets products.
        /// </summary>
        /// <value>
        /// <placeholder>Products.</placeholder>
        /// </value>
        public IEnumerable<Product> Products { get; set; }
    }
}
