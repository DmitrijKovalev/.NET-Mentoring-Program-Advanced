using dotNetTips.Utility.Standard.Tester;
using Moq;
using OnlineStore.CatalogService.Domain.Common.Exceptions;
using OnlineStore.CatalogService.Domain.Common.Models;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Interfaces;
using OnlineStore.CatalogService.Domain.Services;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace OnlineStore.CatalogService.Domain.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class ProductServiceTests
    {
        [Fact]
        public void GivenGetAllProducts_WhenPaginationIsNull_ShouldReturnAllProducts()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "One" },
                new Product { Id = 2, Name = "Two" },
            }.AsQueryable();

            productRepository.Setup(repository => repository.GetAll()).Returns(expectedProducts);

            var service = new ProductService(productRepository.Object, categoryRepository.Object);

            // Act
            var returnedProducts = service.GetAllProducts();

            // Assert
            returnedProducts.ShouldBeEquivalentTo(expectedProducts.ToList());
        }

        [Fact]
        public void GivenGetAllProducts_WhenPaginationIsNotNull_ShouldReturnProducts()
        {
            // Arrange
            var pagination = new Pagination(2, 1);
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var allProducts = new List<Product>
            {
                new Product { Id = 1, Name = "One" },
                new Product { Id = 2, Name = "Two" },
                new Product { Id = 3, Name = "Three" },
            }.AsQueryable();

            var expectedProduct = allProducts
                .Skip(pagination.SkipCount)
                .Take(pagination.PageSize)
                .ToList();

            productRepository.Setup(repository => repository.GetAll()).Returns(allProducts);

            var service = new ProductService(productRepository.Object, categoryRepository.Object);

            // Act
            var returnedProducts = service.GetAllProducts(pagination);

            // Assert
            returnedProducts.ShouldBeEquivalentTo(expectedProduct);
        }

        [Fact]
        public async Task GivenGetProductById_WhenProductExists_ShouldReturnProduct()
        {
            // Arrange
            var productId = 1;
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var expectedProduct = new Product { Id = productId, Name = "One" };

            productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(expectedProduct))
                .Verifiable();

            var service = new ProductService(productRepository.Object, categoryRepository.Object);

            // Act
            var returnedProduct = await service.GetProductByIdAsync(productId);

            // Assert
            returnedProduct.ShouldBeEquivalentTo(expectedProduct);
        }

        [Fact]
        public async Task GivenGetProductById_WhenProductDoesNotExists_ShouldThrowException()
        {
            // Arrange
            var productId = 1;
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();

            productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Product>(null))
                .Verifiable();

            var service = new ProductService(productRepository.Object, categoryRepository.Object);

            // Act
            var action = () => service.GetProductByIdAsync(productId);

            // Assert
            var exception = await Should.ThrowAsync<ProductNotFoundException>(action);
        }

        [Fact]
        public async Task GivenAddProduct_WhenProductIsNull_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var service = new ProductService(productRepository.Object, categoryRepository.Object);

            // Act
            var action = () => service.AddProductAsync(null);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentNullException>(action);
            productRepository.Verify(repository => repository.InsertAsync(null), Times.Never());
        }

        [Fact]
        public async Task GivenAddProduct_WhenProductNameIsNull_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var service = new ProductService(productRepository.Object, categoryRepository.Object);
            var product = new Product();

            // Act
            var action = () => service.AddProductAsync(product);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentNullException>(action);
            productRepository.Verify(repository => repository.InsertAsync(product), Times.Never());
        }

        [Fact]
        public async Task GivenAddProduct_WhenProductNameIsEmpty_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var service = new ProductService(productRepository.Object, categoryRepository.Object);
            var product = new Product
            {
                Name = string.Empty,
            };

            // Act
            var action = () => service.AddProductAsync(product);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentNullException>(action);
            productRepository.Verify(repository => repository.InsertAsync(product), Times.Never());
        }

        [Fact]
        public async Task GivenAddProduct_WhenNameLengthIsMoreThan50Characters_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var service = new ProductService(productRepository.Object, categoryRepository.Object);
            var product = new Product
            {
                Name = RandomData.GenerateWord(51),
            };

            // Act
            var action = () => service.AddProductAsync(product);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentException>(action);
            productRepository.Verify(repository => repository.InsertAsync(product), Times.Never());
        }

        [Fact]
        public async Task GivenAddProduct_WhenItemImageUrlIsNotValid_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var service = new ProductService(productRepository.Object, categoryRepository.Object);
            var product = new Product
            {
                Name = "One",
                ImageUrl = "Invalid image url",
            };

            // Act
            var action = () => service.AddProductAsync(product);

            // Assert
            var exception = await Should.ThrowAsync<UriFormatException>(action);
            productRepository.Verify(repository => repository.InsertAsync(product), Times.Never());
        }

        [Fact]
        public async Task GivenAddProduct_WhenPriceIsBelowZero_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var service = new ProductService(productRepository.Object, categoryRepository.Object);
            var product = new Product
            {
                Name = "One",
                Price = -10,
            };

            // Act
            var action = () => service.AddProductAsync(product);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentException>(action);
            productRepository.Verify(repository => repository.InsertAsync(product), Times.Never());
        }

        [Fact]
        public async Task GivenAddProduct_WhenAmountIsBelowZero_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var productRepository = new Mock<IRepository<Product>>();
            var service = new ProductService(productRepository.Object, categoryRepository.Object);
            var product = new Product
            {
                Name = "One",
                Amount = -100,
            };

            // Act
            var action = () => service.AddProductAsync(product);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentException>(action);
            productRepository.Verify(repository => repository.InsertAsync(product), Times.Never());
        }

        [Fact]
        public async Task GivenAddProduct_WhenCategoryDoesNotExists_ShouldThrowException()
        {
            // Arrange
            var productRepository = new Mock<IRepository<Product>>();
            var categoryRepository = new Mock<IRepository<Category>>();
            categoryRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Category>(null))
                .Verifiable();

            var service = new ProductService(productRepository.Object, categoryRepository.Object);
            var product = new Product
            {
                Name = "One",
            };

            // Act
            var action = () => service.AddProductAsync(product);

            // Assert
            var exception = await Should.ThrowAsync<CategoryNotFoundException>(action);
            productRepository.Verify(repository => repository.InsertAsync(product), Times.Never());
        }

        [Fact]
        public async Task GivenAddProduct_WhenProductIsValid_ShouldAddProductSuccessfully()
        {
            // Arrange
            var productRepository = new Mock<IRepository<Product>>();
            var categoryRepository = new Mock<IRepository<Category>>();
            categoryRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new Category()))
                .Verifiable();

            var service = new ProductService(productRepository.Object, categoryRepository.Object);
            var product = new Product
            {
                Name = "One",
            };

            // Act
            await service.AddProductAsync(product);

            // Assert
            productRepository.Verify(repository => repository.InsertAsync(product), Times.Once());
        }
    }
}
