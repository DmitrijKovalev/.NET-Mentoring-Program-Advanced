using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.WebApi.Tests.Integration.Data
{
    public static class CategoriesControllerTestsData
    {
        public static CategoryViewModel Category => new CategoryViewModel
        {
            Name = "Books",
            ImageUrl = "http://images.com/book.png",
        };

        public static CategoryViewModel CategoryWithoutName => new CategoryViewModel
        {
            ImageUrl = "http://images.com/book.png",
        };

        public static CategoryViewModel CategoryWithWrongImageUrl => new CategoryViewModel
        {
            Name = "Books",
            ImageUrl = "wrong images URL",
        };
    }
}
