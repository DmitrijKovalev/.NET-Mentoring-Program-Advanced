using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;

namespace OnlineStore.CartService.DataAccessLayer
{
    /// <summary>
    /// Cart repository.
    /// </summary>
    internal class CartRepository : ICartRepository
    {
        /// <inheritdoc/>
        public Task<Cart> GetCartByIdAsync(string cartId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task CreateCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UpdateCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DeleteCartByIdAsync(string cartId)
        {
            throw new NotImplementedException();
        }
    }
}
