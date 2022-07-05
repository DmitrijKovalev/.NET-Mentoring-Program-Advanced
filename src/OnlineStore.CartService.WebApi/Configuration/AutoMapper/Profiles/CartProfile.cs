using AutoMapper;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.WebApi.Models.CartViewModels;

namespace OnlineStore.CartService.WebApi.Configuration.AutoMapper.Profiles
{
    /// <summary>
    /// Cart AutoMapper profile.
    /// </summary>
    public class CartProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartProfile"/> class.
        /// </summary>
        public CartProfile()
        {
            this
                .CreateMap<Cart, CartViewModel>()
                .ReverseMap();
        }
    }
}
