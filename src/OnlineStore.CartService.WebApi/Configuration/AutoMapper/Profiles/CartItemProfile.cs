using AutoMapper;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.WebApi.Models.CartViewModels;

namespace OnlineStore.CartService.WebApi.Configuration.AutoMapper.Profiles
{
    /// <summary>
    /// Cart Item AutoMapper profile.
    /// </summary>
    public class CartItemProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemProfile"/> class.
        /// </summary>
        public CartItemProfile()
        {
            this
                .CreateMap<CartItem, CartItemViewModel>()
                .ReverseMap();
        }
    }
}
