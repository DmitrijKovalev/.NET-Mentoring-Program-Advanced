using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Domain.Interfaces
{
    /// <summary>
    /// Product service interface.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>List of products.</returns>
        public IEnumerable<Product> GetAllProducts();

        /// <summary>
        /// Get list of products by category Id.
        /// </summary>
        /// <param name="categoryId">The category Id.</param>
        /// <returns>List of products.</returns>
        public Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Get product by Id.
        /// </summary>
        /// <param name="productId">The product Id.</param>
        /// <returns>The product.</returns>
        public Task<Product> GetProductByIdAsync(int productId);

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public Task AddProductAsync(Product product);

        /// <summary>
        /// Update existing product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public Task UpdateProductAsync(Product product);

        /// <summary>
        /// Delete product by Id.
        /// </summary>
        /// <param name="productId">The product Id.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public Task DeleteProductAsync(int productId);
    }
}
