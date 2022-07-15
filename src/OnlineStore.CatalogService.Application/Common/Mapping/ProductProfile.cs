using AutoMapper;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Application.Common.Mapping
{
    /// <summary>
    /// Product mapping profile.
    /// </summary>
    public class ProductProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductProfile"/> class.
        /// </summary>
        public ProductProfile()
        {
            this
                .CreateMap<Product, ProductViewModel>()
                .ReverseMap();
        }
    }
}
