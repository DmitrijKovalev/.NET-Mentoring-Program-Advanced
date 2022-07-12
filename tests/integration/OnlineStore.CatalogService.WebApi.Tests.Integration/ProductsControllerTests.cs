using FluentAssertions;
using OnlineStore.CatalogService.Application.Categories.Commands.AddCategory;
using OnlineStore.CatalogService.Application.Common.Models;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;
using OnlineStore.CatalogService.Application.Products.Commands.AddProduct;
using OnlineStore.CatalogService.Application.ViewModels;
using OnlineStore.CatalogService.WebApi.Tests.Integration.Data;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace OnlineStore.CatalogService.WebApi.Tests.Integration
{
    public class ProductsControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task GivenAddNewProduct_WhenProductIsValid_ShouldAddProductSuccessfully()
        {
            // Arrange
            var category = await this.AddCategoryAsync();
            var product = ProductsControllerTestsData.GetProduct(category.Id);

            // Act
            var response = await this.HttpClient.PostAsJsonAsync("api/v1/products", product);
            var result = await response.Content.ReadFromJsonAsync<AddProductCommandResult>();
            product.Id = result.ProductId;

            var products = await this.HttpClient.GetFromJsonAsync<PaginatedList<ProductViewModel>>($"/api/v1/products?categoryId={category.Id}");

            // Assert
            result.Success.Should().BeTrue();
            products.Items.Should().HaveCount(1);
            products.Items.FirstOrDefault().Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task GivenAddNewProduct_WhenProductNameIsNotValid_ShouldReturnBadRequest()
        {
            // Arrange
            var category = await this.AddCategoryAsync();
            var product = ProductsControllerTestsData.GetProductWithoutName(category.Id);

            // Act
            var response = await this.HttpClient.PostAsJsonAsync("api/v1/products", product);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GivenAddNewProduct_WhenProductImageUrlIsNotValid_ShouldReturnBadRequest()
        {
            // Arrange
            var category = await this.AddCategoryAsync();
            var product = ProductsControllerTestsData.GetProductWithWrongImageUrl(category.Id);

            // Act
            var response = await this.HttpClient.PostAsJsonAsync("api/v1/products", product);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GivenUpdateProduct_WhenProductExists_ShouldUpdateProductSuccessfully()
        {
            // Arrange
            var category = await this.AddCategoryAsync();
            var product = ProductsControllerTestsData.GetProduct(category.Id);

            // Act
            var addResponse = await this.HttpClient.PostAsJsonAsync("api/v1/products", product);
            var addResult = await addResponse.Content.ReadFromJsonAsync<AddProductCommandResult>();

            product.Id = addResult.ProductId;
            product.Name = "Updated Product Name";

            var updateResponse = await this.HttpClient.PutAsJsonAsync("api/v1/products", product);
            var updateResult = await updateResponse.Content.ReadFromJsonAsync<CommandResponseModel>();

            var products = await this.HttpClient.GetFromJsonAsync<PaginatedList<ProductViewModel>>($"/api/v1/products?categoryId={category.Id}");

            // Assert
            updateResult.Success.Should().BeTrue();
            products.Items.Should().HaveCount(1);
            products.Items.FirstOrDefault().Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task GivenDeleteProduct_WhenProductExists_ShouldDeleteProductSuccessfully()
        {
            // Arrange
            var category = await this.AddCategoryAsync();
            var product = ProductsControllerTestsData.GetProduct(category.Id);

            // Act
            var addResponse = await this.HttpClient.PostAsJsonAsync("api/v1/products", product);
            var addResult = await addResponse.Content.ReadFromJsonAsync<AddProductCommandResult>();

            product.Id = addResult.ProductId;

            var deleteResponse = await this.HttpClient.DeleteAsync($"api/v1/products/{product.Id}");
            var deleteResult = await deleteResponse.Content.ReadFromJsonAsync<CommandResponseModel>();

            var products = await this.HttpClient.GetFromJsonAsync<PaginatedList<ProductViewModel>>($"/api/v1/products?categoryId={category.Id}");

            // Assert
            deleteResult.Success.Should().BeTrue();
            products.Items.Should().HaveCount(0);
        }

        private async Task<CategoryViewModel> AddCategoryAsync()
        {
            var category = CategoriesControllerTestsData.Category;
            var response = await this.HttpClient.PostAsJsonAsync("api/v1/categories", category);
            var result = await response.Content.ReadFromJsonAsync<AddCategoryCommandResult>();
            category.Id = result.CategoryId;

            return category;
        }
    }
}
