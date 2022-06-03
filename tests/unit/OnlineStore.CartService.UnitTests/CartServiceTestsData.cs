using OnlineStore.CartService.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.CartService.UnitTests
{
    [ExcludeFromCodeCoverage]
    public static class CartServiceTestsData
    {
        public static Cart GetEmptyCart(string cartId) => new Cart
        {
            Id = cartId,
        };

        public static Cart GetCart(string cartdId) => new Cart
        {
            Id = cartdId,
            CartItems = new List<CartItem>
            {
                new CartItem { Id = 1, Name = "Book", Price = 10.0m, Quantity = 2 },
                new CartItem { Id = 1, Name = "Magazine", Price = 5.0m, Quantity = 1 },
            }
        };
    }
}
