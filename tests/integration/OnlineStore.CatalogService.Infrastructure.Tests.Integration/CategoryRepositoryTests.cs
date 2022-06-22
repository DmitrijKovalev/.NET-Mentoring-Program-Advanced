using dotNetTips.Utility.Standard.Tester;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Infrastructure.Tests.Integration.TestsFixture;
using OnlineStore.CatalogService.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace OnlineStore.CatalogService.Infrastructure.Tests.Integration
{
    [Collection(nameof(FixtureCollection))]
    public class CategoryRepositoryTests : RepositoyTestsBase
    {
        public CategoryRepositoryTests(Fixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GivenInsertCategory_WhenCategoryParentIdIsNull_ShouldInsertCategorySuccessfully()
        {
            // Arrange
            var categoryRepository = new EfRepository<Category>(this.DataBaseFactory);
            var category = new Category
            {
                Name = "New Category",
            };

            // Act
            await categoryRepository.InsertAsync(category);
            var insertedCategory = await categoryRepository.GetByIdAsync(category.Id);
            var countOfCategories = categoryRepository.GetAll().ToList().Count;

            // Assert
            insertedCategory.Should().BeEquivalentTo(category);
            countOfCategories.ShouldBe(1);
        }

        [Fact]
        public async Task GivenInsertCategory_WhenCategoryParentIdExists_ShouldInsertCategorySuccessfully()
        {
            // Arrange
            var categoryRepository = new EfRepository<Category>(this.DataBaseFactory);
            var parentCategory = new Category
            {
                Name = "Parent Category",
            };

            await categoryRepository.InsertAsync(parentCategory);

            var childCategory = new Category
            {
                Name = "Child Category",
                ParentCategoryId = parentCategory.Id,
            };

            // Act
            await categoryRepository.InsertAsync(childCategory);

            var insertedChildCategory = categoryRepository
                .GetAll()
                .Include(category => category.ParentCategory)
                .FirstOrDefault(category => category.Id == childCategory.Id);

            var countOfCategories = categoryRepository.GetAll().ToList().Count;

            // Assert
            insertedChildCategory.ParentCategory.Id.ShouldBe(parentCategory.Id);
            insertedChildCategory.Name.Should().Be(childCategory.Name);
            countOfCategories.ShouldBe(2);
        }

        [Fact]
        public async Task GivenInsertCategory_WhenCategoryParentIdDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new EfRepository<Category>(this.DataBaseFactory);
            var childCategory = new Category
            {
                Name = "Child Category",
                ParentCategoryId = 0,
            };

            // Act
            var action = () => categoryRepository.InsertAsync(childCategory);

            // Assert
            await Should.ThrowAsync<DbUpdateException>(action);
        }

        [Fact]
        public async Task GivenInsertCategory_WhenNameLengthIsMoreThan50Characters_ShouldThrowException()
        {
            // Arrange
            var categoryRepository = new EfRepository<Category>(this.DataBaseFactory);
            var parentCategory = new Category
            {
                Name = "Parent Category",
            };

            await categoryRepository.InsertAsync(parentCategory);

            var childCategory = new Category
            {
                Name = RandomData.GenerateWord(51),
                ParentCategoryId = parentCategory.Id,
            };

            // Act
            var action = () => categoryRepository.InsertAsync(childCategory);

            // Assert
            await Should.ThrowAsync<DbUpdateException>(action);
        }

        [Fact]
        public async Task GivenUpdateCategory_WhenCategoryExists_ShouldUpdateCategorySuccessfully()
        {
            // Arrange
            var imageUrl = "http://image-domain.com/image";
            var categoryRepository = new EfRepository<Category>(this.DataBaseFactory);
            var parentCategory = new Category
            {
                Name = "Parent Category",
            };

            await categoryRepository.InsertAsync(parentCategory);

            // Act
            var insertedCategory = await categoryRepository.GetByIdAsync(parentCategory.Id);
            insertedCategory.ImageUrl = imageUrl;

            await categoryRepository.UpdateAsync(insertedCategory);

            var updatedCategory = await categoryRepository.GetByIdAsync(parentCategory.Id);

            // Assert
            updatedCategory.ImageUrl.Should().Be(imageUrl);
        }

        [Fact]
        public async Task GivenDeleteCategory_WhenCategoryExists_ShouldDeleteCategorySuccessfully()
        {
            // Arrange
            var categoryRepository = new EfRepository<Category>(this.DataBaseFactory);
            var parentCategory = new Category
            {
                Name = "Parent Category",
            };

            await categoryRepository.InsertAsync(parentCategory);

            // Act
            var insertedCategory = await categoryRepository.GetByIdAsync(parentCategory.Id);

            await categoryRepository.DeleteAsync(insertedCategory);

            var deletedCategory = await categoryRepository.GetByIdAsync(parentCategory.Id);

            // Assert
            deletedCategory.ShouldBeNull();
        }
    }
}
