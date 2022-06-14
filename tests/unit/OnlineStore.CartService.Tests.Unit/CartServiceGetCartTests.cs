using Moq;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using OnlineStore.CartService.Core.Exceptions;
using Shouldly;
using OnlineStore.CartService.Tests.Unit.Data;

namespace OnlineStore.CartService.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    public class CartServiceGetCartTests
    {
        [Fact]
        public async Task GivenGetCart_WhenCartIdIsNull_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.GetCartByIdAsync(null);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenGetCart_WhenCartIdIsEmpty_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.GetCartByIdAsync(string.Empty);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenGetCart_WhenCartDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<Cart>(null))
                .Verifiable();
            var cartId = Guid.NewGuid().ToString();
            var expectedExceptionMessage = $"Cart Not Found. Cart Id: {cartId}.";
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.GetCartByIdAsync(cartId);

            // Assert
            var exception = await Should.ThrowAsync<CartNotFoundException>(action);
            exception.Message.ShouldBe(expectedExceptionMessage);
        }

        [Fact]
        public async Task GivenGetCart_WhenCartExists_ShouldReturnsCart()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cart = CartServiceTestsData.GetCart(cartId);
            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(cart))
                .Verifiable();

            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var returnedCart = await cartService.GetCartByIdAsync(cartId);

            // Assert
            returnedCart.ShouldBeSameAs(cart);
        }
    }
}
