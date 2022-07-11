namespace OnlineStore.CatalogService.Application.ViewModels
{
    /// <summary>
    /// The product.
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Gets or sets id. Not used to create a category.
        /// </summary>
        /// <value>
        /// <placeholder>Id.</placeholder>
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets product name.
        /// </summary>
        /// <value>
        /// <placeholder>Product name.</placeholder>
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product description.
        /// </summary>
        /// <value>
        /// <placeholder>Product description.</placeholder>
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets image URL.
        /// </summary>
        /// <value>
        /// <placeholder>Image URL.</placeholder>
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets product price.
        /// </summary>
        /// <value>
        /// <placeholder>Product price.</placeholder>
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets amount.
        /// </summary>
        /// <value>
        /// <placeholder>Amount.</placeholder>
        /// </value>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets category Id.
        /// </summary>
        /// <value>
        /// <placeholder>Category Id.</placeholder>
        /// </value>
        public int CategoryId { get; set; }
    }
}
