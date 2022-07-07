using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.WebApi.Configuration.Validation;
using OnlineStore.CartService.WebApi.Models.CartViewModels;

namespace OnlineStore.CartService.WebApi.Controllers.V1
{
    /// <summary>
    /// Cart controller (Version 1.0).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICartService cartService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="mapper">Mapper.</param>
        /// <param name="cartService">Cart service.</param>
        public CartController(IMapper mapper, ICartService cartService)
        {
            this.mapper = mapper;
            this.cartService = cartService;
        }

        /// <summary>
        /// Gets cart info by cart id.
        /// </summary>
        /// <param name="cartId">The cart Id.</param>
        /// <returns>Cart information.</returns>
        [HttpGet]
        [Route("{cartId}")]
        public async Task<ActionResult<CartViewModel>> GetCartAsync([FromRoute] string cartId)
        {
            var cart = await this.cartService.GetCartByIdAsync(cartId);
            var result = this.mapper.Map<CartViewModel>(cart);
            return this.Ok(result);
        }

        /// <summary>
        /// Add item to cart.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <param name="cartItem">The item to add.</param>
        /// <returns>Result of operation.</returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Route("{cartId}/add-item")]
        public async Task<ActionResult> AddItemToCartAsync([FromRoute] string cartId, [FromBody] CartItemViewModel cartItem)
        {
            var coreCartItem = this.mapper.Map<CartItem>(cartItem);
            await this.cartService.AddItemToCartAsync(cartId, coreCartItem);
            return this.Ok();
        }

        /// <summary>
        /// Remove item from cart.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <param name="itemId">The item id to remove.</param>
        /// <returns>Result of operation.</returns>
        [HttpDelete]
        [Route("{cartId}/items/{itemId}")]
        public async Task<ActionResult> RemoveItemFromCartAsync([FromRoute] string cartId, [FromRoute] int itemId)
        {
            await this.cartService.RemoveItemFromCartAsync(cartId, itemId);
            return this.Ok();
        }
    }
}
