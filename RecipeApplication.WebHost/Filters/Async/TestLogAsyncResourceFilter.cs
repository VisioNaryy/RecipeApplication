using Microsoft.AspNetCore.Mvc.Filters;

namespace RecipeApplication.Filters.Async;

public class TestLogAsyncResourceFilter : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        Console.WriteLine("Executing async!");
        var executedContext = await next();
        Console.WriteLine("Executed async!");
    }
}