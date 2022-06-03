using OnlineStore.CartService.Core.Models;

namespace OnlineStore.CartService.Core.Interfaces
{
    /// <summary>
    /// Cart service interface.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Gets cart by id asynchronously.
        /// </summary>
        /// <param name="cartId">Cart id.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task<Cart> GetCartByIdAsync(string cartId);

        /// <summary>
        /// Adds item into cart asynchronously.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <param name="item">Item for adding.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task AddItemToCartAsync(string cartId, CartItem item);

        /// <summary>
        /// Removes item from cart asynchronously.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <param name="itemId">The item id.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task RemoveItemFromCartAsync(string cartId, int itemId);

        /// <summary>
        /// Removes existing cart by id.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task RemoveCartAsync(string cartId);
    }
}
