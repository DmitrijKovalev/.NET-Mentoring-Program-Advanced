namespace OnlineStore.CartService.Core.Models
{
    /// <summary>
    /// Model for cart.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets or sets unique identification.
        /// </summary>
        /// <value>
        /// <placeholder>Unique identification.</placeholder>
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets cart items.
        /// </summary>
        /// <value>
        /// <placeholder>Cart items.</placeholder>
        /// </value>
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
