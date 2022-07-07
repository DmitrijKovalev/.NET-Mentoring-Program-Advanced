using AutoMapper;
using MediatR;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Update product command handler.
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, CommandResponseModel>
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="productService">The product service.</param>
        public UpdateProductCommandHandler(
            IMapper mapper,
            IProductService productService)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        /// <inheritdoc/>
        public async Task<CommandResponseModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = this.mapper.Map<Product>(request.Product);
            await this.productService.UpdateProductAsync(productToUpdate);

            return CommandResponseModel.Default;
        }
    }
}
