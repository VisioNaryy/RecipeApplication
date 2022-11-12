using FluentValidation;
using RecipeApplication.Models;

namespace RecipeApplication.Validation;

public class IngredientToCreateValidator : AbstractValidator<IngredientToCreate>
{
    public IngredientToCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Quantity)
            .InclusiveBetween(0, int.MaxValue);

        RuleFor(x => x.Unit)
            .MaximumLength(20);
    }
}