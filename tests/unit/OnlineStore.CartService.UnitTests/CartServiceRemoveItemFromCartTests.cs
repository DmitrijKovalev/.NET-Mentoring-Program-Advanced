using Moq;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using OnlineStore.CartService.Core.Exceptions;
using Shouldly;
using OnlineStore.CartService.UnitTests.Data;

namespace OnlineStore.CartService.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class CartServiceRemoveItemFromCartTests
    {
        [Fact]
        public async Task GivenRemoveItemFromCart_WhenCartIdIsNull_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            var cartItemId = 10;
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.RemoveItemFromCartAsync(null, cartItemId);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenRemoveItemFromCart_WhenCartIdIsEmpty_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            var cartItemId = 10;
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.RemoveItemFromCartAsync(string.Empty, cartItemId);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenRemoveItemFromCart_WhenCartDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var cartItemId = 10;
            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<Cart>(null))
                .Verifiable();
            var cartId = Guid.NewGuid().ToString();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.RemoveItemFromCartAsync(cartId, cartItemId);

            // Assert
            var exception = await Should.ThrowAsync<CartNotFoundException>(action);
        }

        [Fact]
        public async Task GivenRemoveItemFromCart_WhenCartDoesNotIncludeThisItem_CartShouldNotBeUpdated()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItemId = 10;
            var existedCart = CartServiceTestsData.GetCart(cartId);

            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(existedCart))
                .Verifiable();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            await cartService.RemoveItemFromCartAsync(cartId, cartItemId);

            // Assert
            cartRepository.Verify(repository => repository.UpdateCartAsync(It.IsAny<Cart>()), Times.Never());
        }

        [Fact]
        public async Task GivenRemoveItemFromCart_WhenCartIncludeThisItems_ShouldUpdateQuantitySuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItemId = 2;
            var existedCart = CartServiceTestsData.GetCart(cartId);
            var initItemQuantity = existedCart.CartItems.FirstOrDefault(item => item.Id == cartItemId).Quantity;

            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(existedCart))
                .Verifiable();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            await cartService.RemoveItemFromCartAsync(cartId, cartItemId);

            // Assert
            var match = (Cart cart) =>
            {
                var isEqualId = cart.Id.Equals(cartId);
                var isItemCountHasNotBeenChanged = cart.CartItems.Count == existedCart.CartItems.Count;

                var updatedItem = cart.CartItems.FirstOrDefault(item => item.Id == cartItemId);
                var isQuantityHasBeenChanged = updatedItem.Quantity == initItemQuantity - 1;

                return isEqualId && isItemCountHasNotBeenChanged && isQuantityHasBeenChanged;
            };

            cartRepository.Verify(repository => repository.UpdateCartAsync(It.Is<Cart>(cart => match(cart))), Times.Once());
        }

        [Fact]
        public async Task GivenRemoveItemFromCart_WhenCartIncludeThisItem_ShouldRemoveItemFromCartSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItemId = 1;
            var existedCart = CartServiceTestsData.GetCart(cartId);
            var initItemsCount = existedCart.CartItems.Count;

            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(existedCart))
                .Verifiable();

            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            await cartService.RemoveItemFromCartAsync(cartId, cartItemId);

            // Assert
            var match = (Cart cart) =>
            {
                var isEqualId = cart.Id.Equals(cartId);
                var isItemCountHasBeenChanged = cart.CartItems.Count == initItemsCount - 1;

                return isEqualId && isItemCountHasBeenChanged;
            };

            cartRepository.Verify(repository => repository.UpdateCartAsync(It.Is<Cart>(cart => match(cart))), Times.Once());
        }
    }
}
