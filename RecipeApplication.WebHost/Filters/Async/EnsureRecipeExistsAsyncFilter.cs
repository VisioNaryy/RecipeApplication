using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeApplication.DataAccess.Repositories;

namespace RecipeApplication.Filters.Async;

public class EnsureRecipeExistsAsyncFilter : IAsyncActionFilter
{
    private readonly IRecipeRepository _recipeRepo;

    public EnsureRecipeExistsAsyncFilter(IRecipeRepository recipeRepo)
    {
        _recipeRepo = recipeRepo;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.ActionArguments.TryGetValue("id", out var id);

        if (id is int recipeId && !await _recipeRepo.DoesEntityExist(recipeId)!)
        {
            context.Result = new NotFoundResult();
        }

        await next();
    }
}

public class EnsureRecipeExistsAsyncAttribute : TypeFilterAttribute
{
    public EnsureRecipeExistsAsyncAttribute()
        : base(typeof(EnsureRecipeExistsAsyncFilter)) {}
}