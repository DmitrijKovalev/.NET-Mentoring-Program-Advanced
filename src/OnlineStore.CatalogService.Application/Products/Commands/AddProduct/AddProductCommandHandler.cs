using AutoMapper;
using MediatR;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Products.Commands.AddProduct
{
    /// <summary>
    /// Add product command handler.
    /// </summary>
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductCommandResult>
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductCommandHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="productService">The product service.</param>
        public AddProductCommandHandler(
            IMapper mapper,
            IProductService productService)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        /// <inheritdoc/>
        public async Task<AddProductCommandResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productToAdd = this.mapper.Map<Product>(request.Product);
            await this.productService.AddProductAsync(productToAdd);

            var result = new AddProductCommandResult
            {
                ProductId = productToAdd.Id,
                Success = true,
            };

            return result;
        }
    }
}
