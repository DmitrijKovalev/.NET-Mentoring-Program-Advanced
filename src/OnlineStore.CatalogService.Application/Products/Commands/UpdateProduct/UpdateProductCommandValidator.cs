using FluentValidation;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Update category command validator.
    /// </summary>
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandValidator"/> class.
        /// </summary>
        /// <param name="productValidator">Product validator.</param>
        public UpdateProductCommandValidator(IValidator<ProductViewModel> productValidator)
        {
            this.RuleFor(query => query.Product).NotNull().SetValidator(productValidator);
        }
    }
}
