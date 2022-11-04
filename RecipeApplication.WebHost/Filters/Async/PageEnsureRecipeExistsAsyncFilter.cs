using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeApplication.DataAccess.Repositories;

namespace RecipeApplication.Filters.Async;

public class PageEnsureRecipeExistsAsyncFilter : IAsyncPageFilter
{
    private readonly IRecipeRepository _recipeRepo;

    public PageEnsureRecipeExistsAsyncFilter(IRecipeRepository recipeRepo)
    {
        _recipeRepo = recipeRepo;
    }
    
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        context.HandlerArguments.TryGetValue("id", out var id);
        
        if (id is int recipeId && !await _recipeRepo.DoesEntityExist(recipeId)!)
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