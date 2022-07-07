using AutoMapper;
using MediatR;
using OnlineStore.CatalogService.Application.Common.Extensions;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Categories.Queries.GetCategoriesWithPagination
{
    /// <summary>
    /// Get list of categories query handler.
    /// </summary>
    public class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryViewModel>>
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoriesWithPaginationQueryHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="categoryService">The category service.</param>
        public GetCategoriesWithPaginationQueryHandler(
            IMapper mapper,
            ICategoryService categoryService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        /// <inheritdoc/>
        public async Task<PaginatedList<CategoryViewModel>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var categories = await this.categoryService
                .GetAllCategories()
                .ToPaginatedListAsync<Category, CategoryViewModel>(request?.Pagination, this.mapper);

            return categories;
        }
    }
}
