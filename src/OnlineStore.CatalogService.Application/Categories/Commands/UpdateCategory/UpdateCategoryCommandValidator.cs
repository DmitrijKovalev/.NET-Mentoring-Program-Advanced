using FluentValidation;
using OnlineStore.CatalogService.Application.ViewModels;

namespace OnlineStore.CatalogService.Application.Categories.Commands.UpdateCategory
{
    /// <summary>
    /// Update category command validator.
    /// </summary>
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCategoryCommandValidator"/> class.
        /// </summary>
        /// <param name="categoryValidator">Category validator.</param>
        public UpdateCategoryCommandValidator(IValidator<CategoryViewModel> categoryValidator)
        {
            this.RuleFor(query => query.Category).NotNull().SetValidator(categoryValidator);
        }
    }
}
