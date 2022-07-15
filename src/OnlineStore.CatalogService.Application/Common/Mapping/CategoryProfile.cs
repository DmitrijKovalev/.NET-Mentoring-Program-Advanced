using AutoMapper;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Application.Common.Mapping
{
    /// <summary>
    /// Product mapping profile.
    /// </summary>
    public class CategoryProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryProfile"/> class.
        /// </summary>
        public CategoryProfile()
        {
            this
                .CreateMap<Category, CategoryViewModel>()
                .ReverseMap();
        }
    }
}
