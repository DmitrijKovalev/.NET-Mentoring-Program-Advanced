namespace OnlineStore.CartService.Core.Models
{
    /// <summary>
    /// Model for cart item.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Gets or sets unique identification.
        /// </summary>
        /// <value>
        /// <placeholder>Unique identification.</placeholder>
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        /// <value>
        /// <placeholder>Name.</placeholder>
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
        /// Gets or sets price for one item.
        /// </summary>
        /// <value>
        /// <placeholder>Price.</placeholder>
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets items quantity.
        /// </summary>
        /// <value>
        /// <placeholder>Items quantity.</placeholder>
        /// </value>
        public int Quantity { get; set; }
    }
}
