using OnlineStore.CartService.Core.Models;

namespace OnlineStore.CartService.Core.Interfaces
{
    /// <summary>
    /// Cart repository interface.
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Gets cart by id asynchronously.
        /// </summary>
        /// <param name="cartId">Cart id.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task<Cart> GetCartByIdAsync(string cartId);

        /// <summary>
        /// Creates cart asynchronously.
        /// </summary>
        /// <param name="cart">The cart model.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task CreateCartAsync(Cart cart);

        /// <summary>
        /// Updates cart asynchronously.
        /// </summary>
        /// <param name="cart">The cart model.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task UpdateCartAsync(Cart cart);

        /// <summary>
        /// Deletes cart asynchronously.
        /// </summary>
        /// <param name="cartId">The cart model.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        Task DeleteCartByIdAsync(string cartId);
    }
}
