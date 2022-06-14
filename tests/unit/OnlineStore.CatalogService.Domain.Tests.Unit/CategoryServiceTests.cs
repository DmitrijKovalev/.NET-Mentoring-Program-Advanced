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

namespace OnlineStore.CatalogService.Domain.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    public class CategoryServiceTests
    {
        [Fact]
        public void GivenGetAllCategories_WhenPaginationIsNull_ShouldReturnAllCategories()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var expectedCategories = new List<Category>
            {
                new Category { Id = 1, Name = "One" },
                new Category { Id = 2, Name = "Two" },
            }.AsQueryable();

            categoryRepository.Setup(repository => repository.GetAll()).Returns(expectedCategories);

            var service = new CategoryService(categoryRepository.Object);

            // Act
            var returnedCategories = service.GetAllCategories();

            // Assert
            returnedCategories.ShouldBeEquivalentTo(expectedCategories.ToList());
        }

        [Fact]
        public void GivenGetAllCategories_WhenPaginationIsNotNull_ShouldReturnCategories()
        {
            // Arrange
            var pagination = new Pagination(2, 1);
            var categoryRepository = new Mock<IRepository<Category>>();
            var allCategories = new List<Category>
            {
                new Category { Id = 1, Name = "One" },
                new Category { Id = 2, Name = "Two" },
                new Category { Id = 3, Name = "Three" },
            }.AsQueryable();

            var expectedCategories = allCategories
                .Skip(pagination.SkipCount)
                .Take(pagination.PageSize)
                .ToList();

            categoryRepository.Setup(repository => repository.GetAll()).Returns(allCategories);

            var service = new CategoryService(categoryRepository.Object);

            // Act
            var returnedCategories = service.GetAllCategories(pagination);

            // Assert
            returnedCategories.ShouldBeEquivalentTo(expectedCategories);
        }

        [Fact]
        public async Task GivenGetCategoryById_WhenCategoryExists_ShouldReturnCategory()
        {
            // Arrange
            var categoryId = 1;
            var categoryRepository = new Mock<IRepository<Category>>();
            var expectedCategory = new Category { Id = categoryId, Name = "One" };

            categoryRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(expectedCategory))
                .Verifiable();

            var service = new CategoryService(categoryRepository.Object);

            // Act
            var returnedCategory = await service.GetCategoryByIdAsync(categoryId);

            // Assert
            returnedCategory.ShouldBeEquivalentTo(expectedCategory);
        }

        [Fact]
        public async Task GivenGetCategoryById_WhenCategoryDoesNotExists_ShouldThrowException()
        {
            // Arrange
            var categoryId = 1;
            var categoryRepository = new Mock<IRepository<Category>>();

            categoryRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Category>(null))
                .Verifiable();

            var service = new CategoryService(categoryRepository.Object);

            // Act
            var action = () => service.GetCategoryByIdAsync(categoryId);

            // Assert
            var exception = await Should.ThrowAsync<CategoryNotFoundException>(action);
        }

        [Fact]
        public async Task GivenAddCategory_WhenCategoryIsNull_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var service = new CategoryService(categoryRepository.Object);

            // Act
            var action = () => service.AddCategoryAsync(null);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentNullException>(action);
            categoryRepository.Verify(repository => repository.InsertAsync(null), Times.Never());
        }

        [Fact]
        public async Task GivenAddCategory_WhenCategoryNameIsNull_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var service = new CategoryService(categoryRepository.Object);
            var category = new Category();

            // Act
            var action = () => service.AddCategoryAsync(category);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentNullException>(action);
            categoryRepository.Verify(repository => repository.InsertAsync(category), Times.Never());
        }

        [Fact]
        public async Task GivenAddCategory_WhenCategoryNameIsEmpty_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var service = new CategoryService(categoryRepository.Object);
            var category = new Category
            {
                Name = string.Empty,
            };

            // Act
            var action = () => service.AddCategoryAsync(category);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentNullException>(action);
            categoryRepository.Verify(repository => repository.InsertAsync(category), Times.Never());
        }

        [Fact]
        public async Task GivenAddCategory_WhenNameLengthIsMoreThan50Characters_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var service = new CategoryService(categoryRepository.Object);
            var category = new Category
            {
                Name = RandomData.GenerateWord(51),
            };

            // Act
            var action = () => service.AddCategoryAsync(category);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentException>(action);
            categoryRepository.Verify(repository => repository.InsertAsync(category), Times.Never());
        }

        [Fact]
        public async Task GivenAddCategory_WhenItemImageUrlIsNotValid_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            var service = new CategoryService(categoryRepository.Object);
            var category = new Category
            {
                Name = "One",
                ImageUrl = "Invalid image url",
            };

            // Act
            var action = () => service.AddCategoryAsync(category);

            // Assert
            var exception = await Should.ThrowAsync<UriFormatException>(action);
            categoryRepository.Verify(repository => repository.InsertAsync(category), Times.Never());
        }

        [Fact]
        public async Task GivenAddCategory_WhenParentCategoryDoesNotExists_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();
            categoryRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Category>(null))
                .Verifiable();

            var service = new CategoryService(categoryRepository.Object);
            var category = new Category
            {
                Name = "One",
                ParentCategoryId = 1,
            };

            // Act
            var action = () => service.AddCategoryAsync(category);

            // Assert
            var exception = await Should.ThrowAsync<CategoryNotFoundException>(action);
            categoryRepository.Verify(repository => repository.InsertAsync(category), Times.Never());
        }

        [Fact]
        public async Task GivenAddCategory_WhenCategoryIsValid_ShouldAddCategorySuccessfully()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();

            var service = new CategoryService(categoryRepository.Object);
            var category = new Category { Name = "One" };

            // Act
            await service.AddCategoryAsync(category);

            // Assert
            categoryRepository.Verify(repository => repository.InsertAsync(category), Times.Once());
        }

        [Fact]
        public async Task GivenUpdateCategory_WhenCategoryIsValid_ShouldUpdateCategorySuccessfully()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();

            var service = new CategoryService(categoryRepository.Object);
            var categoryForUpdating = new Category { Id = 1, Name = "One" };

            categoryRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new Category { Id = 1 }))
                .Verifiable();

            // Act
            await service.UpdateCategoryAsync(categoryForUpdating);

            // Assert
            var match = (Category category) =>
            {
                var isEqualId = category.Id == categoryForUpdating.Id;
                var isEquilName = category.Name.Equals(categoryForUpdating.Name);

                return isEqualId && isEquilName;
            };
            categoryRepository.Verify(repository => repository.UpdateAsync(It.Is<Category>(category => match(category))), Times.Once());
        }

        [Fact]
        public async Task GivenDeleteCategory_WhenCategoryIsValid_ShouldUpdateCategorySuccessfully()
        {
            // Arrange
            var categoryRepository = new Mock<IRepository<Category>>();

            var service = new CategoryService(categoryRepository.Object);
            var categoryToDelete = new Category { Id = 1, Name = "One" };

            categoryRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(categoryToDelete))
                .Verifiable();

            // Act
            await service.DeleteCategoryAsync(categoryToDelete.Id);

            // Assert
            categoryRepository.Verify(repository => repository.DeleteAsync(categoryToDelete), Times.Once());
        }
    }
}
