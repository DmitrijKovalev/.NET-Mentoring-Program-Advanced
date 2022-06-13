using OnlineStore.CatalogService.Domain.Common.Exceptions;
using OnlineStore.CatalogService.Domain.Common.Models;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Domain.Services
{
    /// <summary>
    /// The category service.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private const int MaxCategotyNameLength = 50;

        private readonly IRepository<Category> categoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="categoryRepository">The category repository.</param>
        public CategoryService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <inheritdoc/>
        public IEnumerable<Category> GetAllCategories(Pagination pagination)
        {
            var categories = this.categoryRepository.GetAll();

            if (pagination is not null)
            {
                categories = categories
                    .Skip(pagination.SkipCount)
                    .Take(pagination.PageSize);
            }

            return categories.ToList();
        }

        /// <inheritdoc/>
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await this.GetCategoryInternalAsync(id);
        }

        /// <inheritdoc/>
        public async Task AddCategoryAsync(Category category)
        {
            await this.ValidateCategoryAsync(category);
            await this.categoryRepository.InsertAsync(category);
        }

        /// <inheritdoc/>
        public async Task UpdateCategoryAsync(Category category)
        {
            await this.ValidateCategoryAsync(category);

            var existingCategory = await this.GetCategoryInternalAsync(category.Id);

            existingCategory.Name = category.Name;
            existingCategory.ImageUrl = category.ImageUrl;
            existingCategory.ParentCategoryId = category.ParentCategoryId;

            await this.categoryRepository.UpdateAsync(existingCategory);
        }

        /// <inheritdoc/>
        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await this.GetCategoryInternalAsync(categoryId);
            await this.categoryRepository.DeleteAsync(category);
        }

        private async Task<Category> GetCategoryInternalAsync(int categoryId)
        {
            var category = await this.categoryRepository.GetByIdAsync(categoryId);

            if (category is null)
            {
                throw new CategoryNotFoundException();
            }

            return category;
        }

        private async Task ValidateCategoryAsync(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (string.IsNullOrEmpty(category.Name))
            {
                throw new ArgumentNullException(nameof(category.Name));
            }

            if (category.Name.Length > MaxCategotyNameLength)
            {
                throw new ArgumentException(nameof(category.Name));
            }

            var isImageUrlProvided = !string.IsNullOrEmpty(category.ImageUrl);

            if (isImageUrlProvided && !Uri.IsWellFormedUriString(category.ImageUrl, UriKind.Absolute))
            {
                throw new UriFormatException(nameof(category.ImageUrl));
            }

            if (category.ParentCategoryId is not null)
            {
                var parentCategory = await this.categoryRepository.GetByIdAsync((int)category.ParentCategoryId);

                if (parentCategory is null)
                {
                    throw new CategoryNotFoundException();
                }
            }
        }
    }
}
