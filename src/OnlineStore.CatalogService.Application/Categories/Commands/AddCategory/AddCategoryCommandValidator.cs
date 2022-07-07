using FluentValidation;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Categories.Commands.AddCategory
{
    /// <summary>
    /// Add category command validator.
    /// </summary>
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCategoryCommandValidator"/> class.
        /// </summary>
        /// <param name="categoryValidator">Category validator.</param>
        public AddCategoryCommandValidator(IValidator<CategoryViewModel> categoryValidator)
        {
            this.RuleFor(query => query.Category).NotNull().SetValidator(categoryValidator);
        }
    }
}
