using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeApplication.DataAccess.Repositories;
using RecipeApplication.Services;

namespace RecipeApplication.Filters.Async;

public class EnsureRecipeExistsAsyncFilter : IAsyncActionFilter
{
    private readonly IRecipesService _service;

    public EnsureRecipeExistsAsyncFilter(IRecipesService service)
    {
        _service = service;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.ActionArguments.TryGetValue("id", out var id);
        
        var recipeRepo = context.HttpContext.RequestServices.GetService(typeof(IRecipeRepository)) as IRecipeRepository;

        if (id is int recipeId && !await recipeRepo?.DoesEntityExist(recipeId)!)
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