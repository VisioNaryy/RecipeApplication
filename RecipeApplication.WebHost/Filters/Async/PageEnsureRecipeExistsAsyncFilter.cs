using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeApplication.DataAccess.Repositories;

namespace RecipeApplication.Filters.Async;

public class PageEnsureRecipeExistsAsyncFilter : IAsyncPageFilter
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        context.HandlerArguments.TryGetValue("id", out var id);
        
        var recipeRepo = context.HttpContext.RequestServices.GetService(typeof(IRecipeRepository)) as IRecipeRepository;

        if (id is int recipeId && !await recipeRepo?.DoesEntityExist(recipeId)!)
        {
            context.Result = new NotFoundResult();
        }

        await next();
    }
}

public class PageEnsureRecipeExistsAsyncAttribute : TypeFilterAttribute
{
    public PageEnsureRecipeExistsAsyncAttribute()
        : base(typeof(PageEnsureRecipeExistsAsyncFilter)) {}
}