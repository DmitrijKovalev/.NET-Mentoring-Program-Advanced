using FluentValidation;
using OnlineStore.CatalogService.Application.Common.Extensions;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Common.Validators
{
    /// <summary>
    /// Category validator.
    /// </summary>
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        private const int MaxProductNameLength = 50;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductValidator"/> class.
        /// </summary>
        public ProductValidator()
        {
            this.RuleFor(product => product.Name)
                .NotEmpty()
                .MaximumLength(MaxProductNameLength);

            this.When(product => !string.IsNullOrEmpty(product.ImageUrl), () =>
            {
                this.RuleFor(product => product.ImageUrl)
                    .Url();
            });

            this.RuleFor(product => product.Price)
                .GreaterThanOrEqualTo(0);

            this.RuleFor(product => product.Amount)
                .GreaterThanOrEqualTo(0);
        }
    }
}
