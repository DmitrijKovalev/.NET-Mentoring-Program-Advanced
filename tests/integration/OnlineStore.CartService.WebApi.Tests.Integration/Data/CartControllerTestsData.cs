using OnlineStore.CartService.WebApi.Models.CartViewModels;

namespace OnlineStore.CartService.WebApi.Tests.Integration.Data
{
    public static class CartControllerTestsData
    {
        public static CartItemViewModel CartItem => new CartItemViewModel
        {
            Id = 1,
            Name = "Book",
            ImageUrl = "http://images.com/book.png",
            Price = 10,
            Quantity = 1,
        };

        public static CartItemViewModel CartItemWithoutName => new CartItemViewModel
        {
            Id = 1,
            ImageUrl = "http://images.com/book.png",
            Price = 10,
            Quantity = 1,
        };

        public static CartItemViewModel CartItemWithWrongImageUrl => new CartItemViewModel
        {
            Id = 1,
            Name = "Book",
            ImageUrl = "wrong images URL",
            Price = 10,
            Quantity = 1,
        };
    }
}
