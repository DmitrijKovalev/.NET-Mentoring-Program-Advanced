using AutoMapper;
using MediatR;
using OnlineStore.CatalogService.Application.Common.Extensions;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Products.Queries.GetProductsByCategoryIdWithPagination
{
    /// <summary>
    /// Get products by category with pagination query handler.
    /// </summary>
    public class GetProductsByCategoryIdWithPaginationQueryHandler : IRequestHandler<GetProductsByCategoryIdWithPaginationQuery, PaginatedList<ProductViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductsByCategoryIdWithPaginationQueryHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="productService">The product service.</param>
        public GetProductsByCategoryIdWithPaginationQueryHandler(
            IMapper mapper,
            IProductService productService)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        /// <inheritdoc/>
        public async Task<PaginatedList<ProductViewModel>> Handle(GetProductsByCategoryIdWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var products = await this.productService
                .GetAllProducts()
                .Where(product => product.CategoryId == request.CategoeyId)
                .ToPaginatedListAsync<Product, ProductViewModel>(request?.Pagination, this.mapper);

            return products;
        }
    }
}
