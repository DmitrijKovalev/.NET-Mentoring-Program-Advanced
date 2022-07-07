using AutoMapper;
using MediatR;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Application.Categories.Commands.AddCategory
{
    /// <summary>
    /// Add category command handler.
    /// </summary>
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, AddCategoryCommandResult>
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCategoryCommandHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="categoryService">The category service.</param>
        public AddCategoryCommandHandler(
            IMapper mapper,
            ICategoryService categoryService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        /// <inheritdoc/>
        public async Task<AddCategoryCommandResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToAdd = this.mapper.Map<Category>(request.Category);
            await this.categoryService.AddCategoryAsync(categoryToAdd);

            var result = new AddCategoryCommandResult
            {
                CategoryId = categoryToAdd.Id,
                Success = true,
            };

            return result;
        }
    }
}
