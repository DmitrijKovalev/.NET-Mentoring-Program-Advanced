using FluentAssertions;
using OnlineStore.CatalogService.Application.Categories.Commands.AddCategory;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.WebApi.Tests.Integration.Data;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace OnlineStore.CatalogService.WebApi.Tests.Integration
{
    public class CategoriesControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task GivenAddNewCategory_WhenCategoryIsValid_ShouldAddCategorySuccessfully()
        {
            // Arrange
            var category = CategoriesControllerTestsData.Category;

            // Act
            var response = await this.HttpClient.PostAsJsonAsync("api/v1/categories", category);
            var result = await response.Content.ReadFromJsonAsync<AddCategoryCommandResult>();

            var categories = await this.HttpClient.GetFromJsonAsync<PaginatedList<CategoryViewModel>>("/api/v1/categories");
            category.Id = result.CategoryId;

            // Assert
            result.Success.Should().BeTrue();
            categories.Items.Should().HaveCount(1);
            categories.Items.FirstOrDefault().Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task GivenAddNewCategory_WhenCategoryNameIsNotValid_ShouldReturnBadRequest()
        {
            // Arrange
            var category = CategoriesControllerTestsData.CategoryWithoutName;

            // Act
            var response = await this.HttpClient.PostAsJsonAsync("api/v1/categories", category);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GivenAddNewCategory_WhenCategoryImageUrlIsNotValid_ShouldReturnBadRequest()
        {
            // Arrange
            var category = CategoriesControllerTestsData.CategoryWithWrongImageUrl;

            // Act
            var response = await this.HttpClient.PostAsJsonAsync("api/v1/categories", category);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GivenUpdateCategory_WhenCategoryExists_ShouldUpdateCategorySuccessfully()
        {
            // Arrange
            var category = CategoriesControllerTestsData.Category;

            // Act
            var addResponse = await this.HttpClient.PostAsJsonAsync("api/v1/categories", category);
            var addResult = await addResponse.Content.ReadFromJsonAsync<AddCategoryCommandResult>();

            category.Id = addResult.CategoryId;
            category.Name = "Updated Name";

            var updateResponse = await this.HttpClient.PutAsJsonAsync("api/v1/categories", category);
            var updateResult = await updateResponse.Content.ReadFromJsonAsync<CommandResponseModel>();

            var categories = await this.HttpClient.GetFromJsonAsync<PaginatedList<CategoryViewModel>>("/api/v1/categories");

            // Assert
            updateResult.Success.Should().BeTrue();
            categories.Items.Should().HaveCount(1);
            categories.Items.FirstOrDefault().Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task GivenDeleteCategory_WhenCategoryExists_ShouldDeleteCategorySuccessfully()
        {
            // Arrange
            var category = CategoriesControllerTestsData.Category;

            // Act
            var addResponse = await this.HttpClient.PostAsJsonAsync("api/v1/categories", category);
            var addResult = await addResponse.Content.ReadFromJsonAsync<AddCategoryCommandResult>();

            category.Id = addResult.CategoryId;

            var deleteResponse = await this.HttpClient.DeleteAsync($"api/v1/categories/{category.Id}");
            var deleteResult = await deleteResponse.Content.ReadFromJsonAsync<CommandResponseModel>();

            var categories = await this.HttpClient.GetFromJsonAsync<PaginatedList<CategoryViewModel>>("/api/v1/categories");

            // Assert
            deleteResult.Success.Should().BeTrue();
            categories.Items.Should().HaveCount(0);
        }
    }
}
