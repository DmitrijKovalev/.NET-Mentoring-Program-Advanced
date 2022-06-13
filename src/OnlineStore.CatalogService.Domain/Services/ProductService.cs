using OnlineStore.CatalogService.Domain.Common.Exceptions;
using OnlineStore.CatalogService.Domain.Common.Models;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;

namespace OnlineStore.CatalogService.Domain.Services
{
    /// <summary>
    /// Product service.
    /// </summary>
    public class ProductService : IProductService
    {
        private const int MaxProductNameLength = 50;

        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Category> categoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="categoryRepository">The category repository.</param>
        public ProductService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        /// <inheritdoc/>
        public IEnumerable<Product> GetAllProducts(Pagination pagination)
        {
            var products = this.productRepository.GetAll();

            if (pagination is not null)
            {
                products = products
                    .Skip(pagination.SkipCount)
                    .Take(pagination.PageSize);
            }

            return products.ToList();
        }

        /// <inheritdoc/>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await this.GetProductInternalAsync(productId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId, Pagination pagination)
        {
            var category = await this.GetCategoryInternalAsync(categoryId);

            var products = this.productRepository
                .GetAll()
                .Where(product => product.CategoryId == category.Id);

            if (pagination is not null)
            {
                products = products
                    .Skip(pagination.SkipCount)
                    .Take(pagination.PageSize);
            }

            return products.ToList();
        }

        /// <inheritdoc/>
        public async Task AddProductAsync(Product product)
        {
            await this.ValidateProductAsync(product);
            await this.productRepository.InsertAsync(product);
        }

        /// <inheritdoc/>
        public async Task UpdateProductAsync(Product product)
        {
            await this.ValidateProductAsync(product);

            var existingProduct = await this.GetProductInternalAsync(product.Id);

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.Price = product.Price;
            existingProduct.Amount = product.Amount;
            existingProduct.CategoryId = product.CategoryId;

            await this.productRepository.UpdateAsync(existingProduct);
        }

        /// <inheritdoc/>
        public async Task DeleteProductAsync(int productId)
        {
            var product = await this.GetProductInternalAsync(productId);
            await this.productRepository.DeleteAsync(product);
        }

        private async Task<Product> GetProductInternalAsync(int productId)
        {
            var product = await this.productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                throw new ProductNotFoundException();
            }

            return product;
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

        private async Task ValidateProductAsync(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentNullException(nameof(product.Name));
            }

            if (product.Name.Length > MaxProductNameLength)
            {
                throw new ArgumentException(nameof(product.Name));
            }

            var isImageUrlProvided = !string.IsNullOrEmpty(product.ImageUrl);

            if (isImageUrlProvided && !Uri.IsWellFormedUriString(product.ImageUrl, UriKind.Absolute))
            {
                throw new UriFormatException(nameof(product.ImageUrl));
            }

            if (product.Price < 0)
            {
                throw new ArgumentException("Price must be greater than or equal to zero");
            }

            if (product.Amount < 0)
            {
                throw new ArgumentException("Amount must be greater than or equal to zero");
            }

            var category = await this.categoryRepository.GetByIdAsync(product.CategoryId);

            if (category is null)
            {
                throw new CategoryNotFoundException();
            }
        }
    }
}
