using FluentAssertions;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.Core.Models.Configuration;
using OnlineStore.CartService.DataAccessLayer;
using OnlineStore.CartService.Tests.Integration.TestsFixture;
using Shouldly;
using Xunit;

namespace OnlineStore.CartService.Tests.Integration
{
    [Collection(nameof(FixtureCollection))]
    public class CartRepositoryTests : RepositoryTestsBase
    {
        public CartRepositoryTests(Fixture mongoDatabaseFixture) : base(mongoDatabaseFixture)
        {
        }

        [Fact]
        public void GivenInitCartRepository_WhenConfigurationIsNull_ShouldThrowException()
        {
            FluentActions.Invoking(() => new CartRepository(null)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GivenInitCartRepository_WhenDatabaseConnectionStringIsNull_ShouldThrowException()
        {
            var configuration = new CartServiceConfiguration
            {
                DatabaseConnectionString = null,
            };

            FluentActions.Invoking(() => new CartRepository(configuration)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GivenInitCartRepository_WhenDatabaseConnectionStringIsEmpty_ShouldThrowException()
        {
            var configuration = new CartServiceConfiguration
            {
                DatabaseConnectionString = string.Empty,
            };

            FluentActions.Invoking(() => new CartRepository(configuration)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenCreateCart_WhenCartDoesNotExist_ShouldCreateCartSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();

            var cart = new Cart()
            {
                Id = cartId,
                CartItems = new List<CartItem>
                {
                    new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1, ImageUrl = "https://images.com/test-image.jpeg" },
                    new CartItem { Id = 2, Name = "Book", Price = 10.0m, Quantity = 2, ImageUrl = "https://images.com/test-image-book.jpeg" },
                },
            };

            var cartRepository = new CartRepository(this.Configuration);

            // Act
            await cartRepository.CreateCartAsync(cart);
            var returnedCart = await cartRepository.GetCartByIdAsync(cartId);
            var countDocuments = this.MongoHelperRepository.CartCollection.CountDocuments(this.MongoHelperRepository.EmptyFilter);

            // Assert
            returnedCart.Should().BeEquivalentTo(cart);
            countDocuments.ShouldBe(1);
        }

        [Fact]
        public async Task GivenUpdateCart_WhenCarExists_ShouldUpdateCartSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();

            var cart = new Cart()
            {
                Id = cartId,
                CartItems = new List<CartItem>
                {
                    new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1, ImageUrl = "https://images.com/test-image.jpeg" },
                    new CartItem { Id = 2, Name = "Book", Price = 10.0m, Quantity = 2, ImageUrl = "https://images.com/test-image-book.jpeg" },
                },
            };

            var cartRepository = new CartRepository(this.Configuration);

            // Act
            await cartRepository.CreateCartAsync(cart);
            cart.CartItems.Clear();
            await cartRepository.UpdateCartAsync(cart);
            var returnedCart = await cartRepository.GetCartByIdAsync(cartId);
            var countDocuments = this.MongoHelperRepository.CartCollection.CountDocuments(this.MongoHelperRepository.EmptyFilter);

            // Assert
            returnedCart.Should().BeEquivalentTo(cart);
            countDocuments.ShouldBe(1);
        }

        [Fact]
        public async Task GivenDeleteCart_WhenCarExists_ShouldDeleteCartSuccessfully()
        {
            // Arrange
            var existedCartId = Guid.NewGuid().ToString();
            var cartIdTodelete = Guid.NewGuid().ToString();

            var existedCart = new Cart()
            {
                Id = existedCartId,
                CartItems = new List<CartItem>
                {
                    new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1, ImageUrl = "https://images.com/test-image.jpeg" },
                    new CartItem { Id = 2, Name = "Book", Price = 10.0m, Quantity = 2, ImageUrl = "https://images.com/test-image-book.jpeg" },
                },
            };

            var cartToDelete = new Cart()
            {
                Id = cartIdTodelete,
                CartItems = new List<CartItem>
                {
                    new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1, ImageUrl = "https://images.com/test-image.jpeg" },
                    new CartItem { Id = 2, Name = "Book", Price = 10.0m, Quantity = 2, ImageUrl = "https://images.com/test-image-book.jpeg" },
                },
            };

            var cartRepository = new CartRepository(this.Configuration);

            // Act
            await cartRepository.CreateCartAsync(existedCart);
            await cartRepository.CreateCartAsync(cartToDelete);
            await cartRepository.DeleteCartByIdAsync(cartIdTodelete);
            var returnedCart = await cartRepository.GetCartByIdAsync(cartIdTodelete);
            var countDocuments = this.MongoHelperRepository.CartCollection.CountDocuments(this.MongoHelperRepository.EmptyFilter);

            // Assert
            returnedCart.Should().BeNull();
            countDocuments.ShouldBe(1);
        }
    }
}
