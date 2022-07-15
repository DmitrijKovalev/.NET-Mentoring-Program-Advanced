using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Domain.Interfaces
{
    /// <summary>
    /// Category service interface.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>List of categories.</returns>
        public IQueryable<Category> GetAllCategories();

        /// <summary>
        /// Get category by Id.
        /// </summary>
        /// <param name="id">The category Id.</param>
        /// <returns>The category.</returns>
        public Task<Category> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Add new category.
        /// </summary>
        /// <param name="category">The category to add.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public Task AddCategoryAsync(Category category);

        /// <summary>
        /// Update existing category.
        /// </summary>
        /// <param name="category">The category to update.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public Task UpdateCategoryAsync(Category category);

        /// <summary>
        /// Delete existing category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public Task DeleteCategoryAsync(int categoryId);
    }
}
