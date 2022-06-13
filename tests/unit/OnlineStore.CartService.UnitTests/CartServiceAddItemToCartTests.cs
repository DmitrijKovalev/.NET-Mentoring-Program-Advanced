using Moq;
using OnlineStore.CartService.Core.Interfaces;
using OnlineStore.CartService.Core.Models;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using Shouldly;
using OnlineStore.CartService.UnitTests.Data;

namespace OnlineStore.CartService.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class CartServiceAddItemToCartTests
    {
        [Fact]
        public async Task GivenAddItemToCart_WhenCartIdIsNull_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            var cartItem = new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1 };
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.AddItemToCartAsync(null, cartItem);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenCartIdIsEmpty_ShouldThrowException()
        {
            // Arrange
            var cartRepository = new Mock<ICartRepository>();
            var cartItem = new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1 };
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.AddItemToCartAsync(string.Empty, cartItem);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenItemIsNull_ShouldThrowException()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.AddItemToCartAsync(cartId, null);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenItemIdIsNotSpecified_ShouldThrowException()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = new CartItem { Name = "Magazine", Price = 5.0m, Quantity = 1 };
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.AddItemToCartAsync(cartId, cartItem);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentException>(action);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenItemNameIsNotSpecified_ShouldThrowException()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = new CartItem { Id = 1, Price = 5.0m, Quantity = 1 };
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.AddItemToCartAsync(cartId, cartItem);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentException>(action);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenItemPriceIsBellowZero_ShouldThrowException()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = new CartItem { Id = 1, Name = "Magazine", Quantity = 1, Price = -10 };
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.AddItemToCartAsync(cartId, cartItem);

            // Assert
            var exception = await Should.ThrowAsync<ArgumentException>(action);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenItemImageUrlIsNotValid_ShouldThrowException()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1, ImageUrl = "Invalid image url" };
            var cartRepository = new Mock<ICartRepository>();
            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            var action = () => cartService.AddItemToCartAsync(cartId, cartItem);

            // Assert
            var exception = await Should.ThrowAsync<UriFormatException>(action);
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenCartDoesNotExist_ShouldCreateNewCart()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1, ImageUrl = "https://images.com/test-image.jpeg" };

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

        [Fact]
        public async Task GivenAddItemToCart_WhenCartWithThisItemAlreadyExist_ShouldUpdateQuantitySuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 3, ImageUrl = "https://images.com/test-image.jpeg" };
            var existedCart = CartServiceTestsData.GetCart(cartId);
            var initItemQuantity = existedCart.CartItems.FirstOrDefault(item => item.Id == cartItem.Id).Quantity;

            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(existedCart))
                .Verifiable();

            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            await cartService.AddItemToCartAsync(cartId, cartItem);

            // Assert
            var match = (Cart cart) =>
            {
                var isEqualId = cart.Id.Equals(cartId);
                var isItemCountHasNotBeenChanged = cart.CartItems.Count == existedCart.CartItems.Count;

                var updatedItem = cart.CartItems.FirstOrDefault(item => item.Id == cartItem.Id);
                var isQuantityHasBeenChanged = updatedItem.Quantity == initItemQuantity + cartItem.Quantity;

                return isEqualId && isItemCountHasNotBeenChanged && isQuantityHasBeenChanged;
            };

            cartRepository.Verify(repository => repository.UpdateCartAsync(It.Is<Cart>(cart => match(cart))), Times.Once());
        }

        [Fact]
        public async Task GivenAddItemToCart_WhenCartExistWithoutThisItem_ShouldAddItemToCartSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cartItem = new CartItem { Id = 3, Name = "Notebook", Price = 5.0m, Quantity = 5, ImageUrl = "https://images.com/test-image-notebook.jpeg" };
            var existedCart = CartServiceTestsData.GetCart(cartId);
            var initItemsCount = existedCart.CartItems.Count;

            var cartRepository = new Mock<ICartRepository>();
            cartRepository
                .Setup(repository => repository.GetCartByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(existedCart))
                .Verifiable();

            var cartService = new BusinessLogicLayer.CartService(cartRepository.Object);

            // Act
            await cartService.AddItemToCartAsync(cartId, cartItem);

            // Assert
            var match = (Cart cart) =>
            {
                var isEqualId = cart.Id.Equals(cartId);
                var isItemCountHasBeenChanged = cart.CartItems.Count == initItemsCount + 1;

                return isEqualId && isItemCountHasBeenChanged;
            };

            cartRepository.Verify(repository => repository.UpdateCartAsync(It.Is<Cart>(cart => match(cart))), Times.Once());
        }
    }
}
