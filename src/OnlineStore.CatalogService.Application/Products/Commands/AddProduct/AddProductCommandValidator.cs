using FluentValidation;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Products.Commands.AddProduct
{
    /// <summary>
    /// Add product command validator.
    /// </summary>
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductCommandValidator"/> class.
        /// </summary>
        /// <param name="productValidator">Product validator.</param>
        public AddProductCommandValidator(IValidator<ProductViewModel> productValidator)
        {
            this.RuleFor(query => query.Product).NotNull().SetValidator(productValidator);
        }
    }
}
