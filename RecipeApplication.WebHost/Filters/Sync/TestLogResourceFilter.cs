using Microsoft.AspNetCore.Mvc.Filters;

namespace RecipeApplication.Filters.Sync;

public class TestLogResourceFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine("Executing!");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine("Executed!");
    }
}