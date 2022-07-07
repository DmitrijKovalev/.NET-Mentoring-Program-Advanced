using MediatR;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Categories.Commands.DeleteCategory
{
    /// <summary>
    /// Delete category command handler.
    /// </summary>
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CommandResponseModel>
    {
        private readonly ICategoryService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryCommandHandler"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public DeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <inheritdoc/>
        public async Task<CommandResponseModel> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await this.categoryService.DeleteCategoryAsync(request.CategoeyId);

            return CommandResponseModel.Default;
        }
    }
}
