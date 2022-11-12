using FluentValidation;
using RecipeApplication.Models;

namespace RecipeApplication.Validation;

public class RecipeBaseValidator : AbstractValidator<RecipeBase>
{
    public RecipeBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.TimeToCookHrs)
            .InclusiveBetween(0, 24);
        
        RuleFor(x => x.TimeToCookMins)
            .InclusiveBetween(0, 59);
    }
}