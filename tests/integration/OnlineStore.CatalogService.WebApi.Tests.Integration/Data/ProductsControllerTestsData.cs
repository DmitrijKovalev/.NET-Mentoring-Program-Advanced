using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.WebApi.Tests.Integration.Data
{
    public static class ProductsControllerTestsData
    {
        public static ProductViewModel GetProduct(int categoryId) => new ProductViewModel
        {
            Name = "Book",
            ImageUrl = "http://images.com/book.png",
            Amount = 10,
            Price = 0,
            CategoryId = categoryId,
        };

        public static ProductViewModel GetProductWithoutName(int categoryId) => new ProductViewModel
        {
            ImageUrl = "http://images.com/book.png",
            Amount = 10,
            Price = 0,
            CategoryId = categoryId,
        };

        public static ProductViewModel GetProductWithWrongImageUrl(int categoryId) => new ProductViewModel
        {
            Name = "Book",
            ImageUrl = "Wrong images URL",
            Amount = 10,
            Price = 0,
            CategoryId = categoryId,
        };
    }
}
