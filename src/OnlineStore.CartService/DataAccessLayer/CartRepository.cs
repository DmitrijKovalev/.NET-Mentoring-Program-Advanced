using MongoDB.Driver;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.Core.Models.Configuration;

namespace OnlineStore.CartService.DataAccessLayer
{
    /// <summary>
    /// Cart repository.
    /// </summary>
    public class CartRepository : BaseRepository, ICartRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CartRepository(CartServiceConfiguration configuration) : base(configuration)
        {
        }

        /// <inheritdoc/>
        public async Task<Cart> GetCartByIdAsync(string cartId)
        {
            return await this.CartCollection?.Find(c => c.Id.Equals(cartId))?.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task CreateCartAsync(Cart cart)
        {
            await this.CartCollection?.InsertOneAsync(cart);
        }

        /// <inheritdoc/>
        public async Task UpdateCartAsync(Cart cart)
        {
            await this.CartCollection?.ReplaceOneAsync(c => c.Id.Equals(cart.Id), cart);
        }

        /// <inheritdoc/>
        public async Task DeleteCartByIdAsync(string cartId)
        {
            await this.CartCollection?.DeleteOneAsync(c => c.Id.Equals(cartId));
        }
    }
}
