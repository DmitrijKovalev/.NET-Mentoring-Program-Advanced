using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.WebApi.Models.CartViewModels;

namespace OnlineStore.CartService.WebApi.Controllers.V2
{
    /// <summary>
    /// Cart controller (Version 2.0).
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2/[controller]")]
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
        /// <returns>List of the cart items.</returns>
        [HttpGet]
        [Route("{cartId}")]
        public async Task<ActionResult<IEnumerable<CartItemViewModel>>> GetCartAsync([FromRoute] string cartId)
        {
            var coreCart = await this.cartService.GetCartByIdAsync(cartId);
            var cartItems = this.mapper.Map<IEnumerable<CartItemViewModel>>(coreCart?.CartItems);
            return this.Ok(cartItems);
        }
    }
}
