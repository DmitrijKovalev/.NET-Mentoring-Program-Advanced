using Moq;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;
using Xunit;
using OnlineStore.CartService.Core.Exceptions;
using Shouldly;
using OnlineStore.CartService.Tests.Unit.Data;

namespace OnlineStore.CartService.Tests.Unit
{
    public class CartServiceRemoveCartTests
    {
        [Fact]
        public async Task GivenRemoveCart_WhenCartIdIsNull_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.RemoveCartAsync(null);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenRemoveCart_WhenCartIdIsEmpty_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.RemoveCartAsync(string.Empty);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenRemoveCart_WhenCartDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<Cart>(null))
                .Verifiable();
            var cartId = Guid.NewGuid().ToString();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.RemoveCartAsync(cartId);

            // Assert
            await Should.ThrowAsync<CartNotFoundException>(action);
        }

        [Fact]
        public async Task GivenRemoveCart_WhenCartExists_ShouldDeleteCartSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cart = CartServiceTestsData.GetEmptyCart(cartId);
            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(cart))
                .Verifiable();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            await cartService.RemoveCartAsync(cartId);

            // Assert
            cartRepository.Verify(repository => repository.DeleteCartByIdAsync(cartId), Times.Once());
        }
    }
}
