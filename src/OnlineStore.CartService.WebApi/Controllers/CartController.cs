using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.WebApi.Configuration.Validation;
using OnlineStore.CartService.WebApi.Models.CartViewModels;

namespace OnlineStore.CartService.WebApi.Controllers
{
    /// <summary>
    /// Cart controller.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [ApiVersion("1.0")]
        [Route("{cartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartViewModel>> GetCartV1Async([FromRoute] string cartId)
        {
            var cart = await this.cartService.GetCartByIdAsync(cartId);
            var result = this.mapper.Map<CartViewModel>(cart);
            return this.Ok(result);
        }

        /// <summary>
        /// Gets cart info by cart id.
        /// </summary>
        /// <param name="cartId">The cart Id.</param>
        /// <returns>List of the cart items.</returns>
        [HttpGet]
        [ApiVersion("2.0")]
        [Route("{cartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CartItemViewModel>>> GetCartV2Async([FromRoute] string cartId)
        {
            var coreCart = await this.cartService.GetCartByIdAsync(cartId);
            var cartItems = this.mapper.Map<IEnumerable<CartItemViewModel>>(coreCart?.CartItems);
            return this.Ok(cartItems);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveItemFromCartAsync([FromRoute] string cartId, [FromRoute] int itemId)
        {
            await this.cartService.RemoveItemFromCartAsync(cartId, itemId);
            return this.Ok();
        }
    }
}
