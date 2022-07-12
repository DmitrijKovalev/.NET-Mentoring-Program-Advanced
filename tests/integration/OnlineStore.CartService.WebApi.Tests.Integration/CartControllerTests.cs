using FluentAssertions;
using OnlineStore.CartService.WebApi.Models.CartViewModels;
using OnlineStore.CartService.WebApi.Tests.Integration.Data;
using OnlineStore.CartService.WebApi.Tests.Integration.TestsFixture;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace OnlineStore.CartService.WebApi.Tests.Integration
{
    [Collection(nameof(FixtureCollection))]
    public class CartControllerTests : CartControllerBase
    {
        public CartControllerTests(Fixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenItemIsValid_ShouldAddItemSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = CartControllerTestsData.CartItem;

            // Act
            await this.HttpClient.PostAsJsonAsync($"/api/v1/cart/{cartId}/add-item", cartItem);
            var returnedCart = await this.HttpClient.GetFromJsonAsync<CartViewModel>($"/api/v1/cart/{cartId}");

            // Assert
            returnedCart.Id.Should().Be(cartId);
            returnedCart.CartItems.Should().HaveCount(1);
            returnedCart.CartItems.FirstOrDefault().Should().BeEquivalentTo(cartItem);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenItemNameIsNotValid_ShouldReturnBadRequest()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = CartControllerTestsData.CartItemWithoutName;

            // Act
            var result = await this.HttpClient.PostAsJsonAsync($"/api/v1/cart/{cartId}/add-item", cartItem);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenItemImageUrlIsNotValid_ShouldReturnBadRequest()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = CartControllerTestsData.CartItemWithWrongImageUrl;

            // Act
            var result = await this.HttpClient.PostAsJsonAsync($"/api/v1/cart/{cartId}/add-item", cartItem);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GivenRemoveItemFromCart_WhenItemIsValid_ShouldRemoveItemSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = CartControllerTestsData.CartItem;

            // Act
            await this.HttpClient.PostAsJsonAsync($"/api/v1/cart/{cartId}/add-item", cartItem);
            await this.HttpClient.DeleteAsync($"/api/v1/cart/{cartId}/items/{cartItem.Id}");

            var returnedCart = await this.HttpClient.GetFromJsonAsync<CartViewModel>($"/api/v1/cart/{cartId}");

            // Assert
            returnedCart.Id.Should().Be(cartId);
            returnedCart.CartItems.Should().HaveCount(0);
        }

        [Fact]
        public async Task GivenGetCartV2_WhenCartExist_ShouldReturnCartSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = CartControllerTestsData.CartItem;

            // Act
            await this.HttpClient.PostAsJsonAsync($"/api/v2/cart/{cartId}/add-item", cartItem);
            var returnedCartItems = await this.HttpClient.GetFromJsonAsync<IEnumerable<CartItemViewModel>>($"/api/v2/cart/{cartId}");

            // Assert
            returnedCartItems.Should().HaveCount(1);
            returnedCartItems.FirstOrDefault().Should().BeEquivalentTo(cartItem);
        }
    }
}
