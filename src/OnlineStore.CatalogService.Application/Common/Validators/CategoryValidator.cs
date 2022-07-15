using FluentValidation;
using OnlineStore.CatalogService.Application.Common.Extensions;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Common.Validators
{
    /// <summary>
    /// Category validator.
    /// </summary>
    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        private const int MaxCategotyNameLength = 50;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryValidator"/> class.
        /// </summary>
        public CategoryValidator()
        {
            this.RuleFor(category => category.Name)
                .NotEmpty()
                .MaximumLength(MaxCategotyNameLength);

            this.When(category => !string.IsNullOrEmpty(category.ImageUrl), () =>
            {
                this.RuleFor(category => category.ImageUrl)
                    .Url();
            });
        }
    }
}
