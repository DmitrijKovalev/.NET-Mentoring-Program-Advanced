using MediatR;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Products.Commands.DeleteProducts
{
    /// <summary>
    /// Delete product command handler.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, CommandResponseModel>
    {
        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductCommandHandler"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public DeleteProductCommandHandler(IProductService productService)
        {
            this.productService = productService;
        }

        /// <inheritdoc/>
        public async Task<CommandResponseModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await this.productService.DeleteProductAsync(request.ProductId);

            return CommandResponseModel.Default;
        }
    }
}
