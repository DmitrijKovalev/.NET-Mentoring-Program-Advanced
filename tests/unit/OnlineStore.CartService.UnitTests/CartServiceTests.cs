using Moq;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.BusinessLogicLayer;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using OnlineStore.CartService.Core.Exceptions;
using Shouldly;

namespace OnlineStore.CartService.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class CartServiceTests
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
            _ = await Should.ThrowAsync<ArgumentNullException>(action);
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
            _ = await Should.ThrowAsync<ArgumentNullException>(action);
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
            Equals(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public async Task GivenGetCart_WhenCartExist_ShouldReturnsCart()
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
            Equals(cart, returnedCart);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenCartDoesNotExist_ShouldReturnNewCart()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1 };

            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<Cart>(null))
                .Verifiable();

            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            await cartService.AddItemToCartAsync(cartId, cartItem);

            // Assert
            var match = (Cart cart) =>
            {
                var isEqualId = cart.Id.Equals(cartId);
                var isOnlyOneItem = cart.CartItems.Count == 1;
                var isCartItemsIncludeAddedItem = cart.CartItems.Any(item => item.Id == 1);
                return isEqualId && isOnlyOneItem && isCartItemsIncludeAddedItem;
            };

            cartRepository.Verify(repository => repository.CreateCartAsync(It.Is<Cart>(cart => match(cart))), Times.Once());
        }
    }
}
