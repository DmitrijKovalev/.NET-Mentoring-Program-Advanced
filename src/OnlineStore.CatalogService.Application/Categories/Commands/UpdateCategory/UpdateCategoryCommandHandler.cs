using AutoMapper;
using MediatR;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Categories.Commands.UpdateCategory
{
    /// <summary>
    /// Update category command handler.
    /// </summary>
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CommandResponseModel>
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCategoryCommandHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="categoryService">The category service.</param>
        public UpdateCategoryCommandHandler(
            IMapper mapper,
            ICategoryService categoryService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        /// <inheritdoc/>
        public async Task<CommandResponseModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToUpdate = this.mapper.Map<Category>(request.Category);
            await this.categoryService.UpdateCategoryAsync(categoryToUpdate);

            return CommandResponseModel.Default;
        }
    }
}
