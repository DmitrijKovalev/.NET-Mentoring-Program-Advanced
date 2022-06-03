﻿using OnlineStore.CartService.BusinessLogicLayer.Exceptions;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;

namespace OnlineStore.CartService.BusinessLogicLayer.Services
{
    /// <summary>
    /// Cart service.
    /// </summary>
    internal class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="cartRepository">Cart repository.</param>
        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        /// <inheritdoc/>
        public async Task<Cart> GetCartByIdAsync(string cartId)
        {
            var cart = await this.cartRepository.GetCartByIdAsync(cartId);

            if (cart is null)
            {
                throw new CartNotFoundException($"Cart Not Found. Cart Id: {cartId}.");
            }

            return cart;
        }

        /// <inheritdoc/>
        public async Task AddItemToCartAsync(string cartId, CartItem item)
        {
            try
            {
                var cart = await this.GetCartByIdAsync(cartId);
                var existingItem = cart.CartItems.FirstOrDefault(cartItem => cartItem.Id == item.Id);
                if (existingItem is null)
                {
                    cart.CartItems.Add(item);
                }
                else
                {
                    existingItem.Quantity += item.Quantity;
                }

                await this.cartRepository.UpdateCartAsync(cart);
            }
            catch (CartNotFoundException)
            {
                var cart = new Cart
                {
                    Id = cartId,
                    CartItems = new List<CartItem> { item },
                };

                await this.cartRepository.CreateCartAsync(cart);
            }
        }

        /// <inheritdoc/>
        public async Task RemoveItemFromCartAsync(string cartId, int itemId)
        {
            var cart = await this.GetCartByIdAsync(cartId);
            var existingItem = cart.CartItems.FirstOrDefault(cartItem => cartItem.Id == itemId);

            if (existingItem is not null)
            {
                if (existingItem.Quantity <= 1)
                {
                    cart.CartItems.Remove(existingItem);
                }
                else
                {
                    existingItem.Quantity -= 1;
                }

                await this.cartRepository.UpdateCartAsync(cart);
            }
        }

        /// <inheritdoc/>
        public async Task RemoveCartAsync(string cartId)
        {
            _ = await this.GetCartByIdAsync(cartId);
            await this.cartRepository.DeleteCartByIdAsync(cartId);
        }
    }
}
