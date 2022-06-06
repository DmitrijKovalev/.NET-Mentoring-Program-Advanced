using OnlineStore.CartService.Core.Models;
using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.CartService.UnitTests.Data
{
    [ExcludeFromCodeCoverage]
    public static class CartServiceTestsData
    {
        public static Cart GetEmptyCart(string cartId) => new ()
        {
            Id = cartId,
        };

        public static Cart GetCart(string cartdId) => new ()
        {
            Id = cartdId,
            CartItems = new List<CartItem>
            {
                new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1, ImageUrl = "https://images.com/test-image.jpeg" },
                new CartItem { Id = 2, Name = "Book", Price = 10.0m, Quantity = 2, ImageUrl = "https://images.com/test-image-book.jpeg" },
            },
        };
    }
}
