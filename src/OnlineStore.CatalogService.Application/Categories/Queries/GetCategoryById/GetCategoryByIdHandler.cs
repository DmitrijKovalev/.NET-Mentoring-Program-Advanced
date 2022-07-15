using AutoMapper;
using MediatR;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Categories.Queries.GetCategoryById
{
    /// <summary>
    /// Get list of categories query handler.
    /// </summary>
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryViewModel>
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="categoryService">The category service.</param>
        public GetCategoryByIdHandler(
            IMapper mapper,
            ICategoryService categoryService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        /// <inheritdoc/>
        public async Task<CategoryViewModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await this.categoryService.GetCategoryByIdAsync(request.CategoryId);
            return this.mapper.Map<CategoryViewModel>(category);
        }
    }
}
