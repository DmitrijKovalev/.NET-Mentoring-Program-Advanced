using dotNetTips.Utility.Standard.Tester;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Infrastructure.Tests.Integration.TestsFixture;
using OnlineStore.CatalogService.Infrastructure.Persistence;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace OnlineStore.CatalogService.Infrastructure.Tests.Integration
{
    [ExcludeFromCodeCoverage]
    [Collection(nameof(FixtureCollection))]
    public class ProductRepositoryTests : RepositoyTestsBase
    {
        public ProductRepositoryTests(Fixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GivenInsertProduct_WhenCategoryExists_ShouldInsertProductSuccessfully()
        {
            // Arrange
            var categoryId = await this.CreateCategoryAsync();
            var productRepository = new EfRepository<Product>(this.DataBaseFactory);

            var product = new Product
            {
                CategoryId = categoryId,
                Name = "Product",
                Price = 10,
                Amount = 5,
            };

            // Act
            await productRepository.InsertAsync(product);
            var insertedProduct = await productRepository.GetByIdAsync(product.Id);
            var countOfProducts = productRepository.GetAll().ToList().Count;

            // Assert
            insertedProduct.Should().BeEquivalentTo(product);
            countOfProducts.ShouldBe(1);
        }

        [Fact]
        public async Task GivenInsertProduct_WhenCategoryDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var productRepository = new EfRepository<Product>(this.DataBaseFactory);
            var product = new Product
            {
                CategoryId = 0,
                Name = "Product",
                Price = 10,
                Amount = 5,
            };

            // Act
            var action = () => productRepository.InsertAsync(product);

            // Assert
            await Should.ThrowAsync<DbUpdateException>(action);
        }

        [Fact]
        public async Task GivenInsertProduct_WhenNameLengthIsMoreThan50Characters_ShouldThrowException()
        {
            // Arrange
            var categoryId = await this.CreateCategoryAsync();
            var productRepository = new EfRepository<Product>(this.DataBaseFactory);

            var product = new Product
            {
                CategoryId = categoryId,
                Name = RandomData.GenerateWord(51),
                Price = 10,
                Amount = 5,
            };

            // Act
            var action = () => productRepository.InsertAsync(product);

            // Assert
            await Should.ThrowAsync<DbUpdateException>(action);
        }

        [Fact]
        public async Task GivenUpdateProduct_WhenProductExists_ShouldUpdateProductSuccessfully()
        {
            // Arrange
            var imageUrl = "http://image-domain.com/image";
            var categoryId = await this.CreateCategoryAsync();
            var productRepository = new EfRepository<Product>(this.DataBaseFactory);

            var product = new Product
            {
                CategoryId = categoryId,
                Name = "Product",
                Price = 10,
                Amount = 5,
            };

            await productRepository.InsertAsync(product);

            // Act
            var insertedProduct = await productRepository.GetByIdAsync(product.Id);
            insertedProduct.ImageUrl = imageUrl;

            await productRepository.UpdateAsync(insertedProduct);

            var updatedProduct = await productRepository.GetByIdAsync(product.Id);

            // Assert
            updatedProduct.ImageUrl.Should().Be(imageUrl);
        }

        [Fact]
        public async Task GivenDeleteProduct_WhenProductExists_ShouldDeleteProductSuccessfully()
        {
            // Arrange
            var categoryId = await this.CreateCategoryAsync();
            var productRepository = new EfRepository<Product>(this.DataBaseFactory);

            var product = new Product
            {
                CategoryId = categoryId,
                Name = "Product",
                Price = 10,
                Amount = 5,
            };

            await productRepository.InsertAsync(product);

            // Act
            var insertedProduct = await productRepository.GetByIdAsync(product.Id);

            await productRepository.DeleteAsync(insertedProduct);

            var deletedProduct = await productRepository.GetByIdAsync(product.Id);

            // Assert
            deletedProduct.ShouldBeNull();
        }

        private async Task<int> CreateCategoryAsync()
        {
            var categoryRepository = new EfRepository<Category>(this.DataBaseFactory);
            var category = new Category
            {
                Name = "New Category",
            };

            await categoryRepository.InsertAsync(category);

            return category.Id;
        }
    }
}
