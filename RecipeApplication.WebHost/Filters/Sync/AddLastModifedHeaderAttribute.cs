using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeApplication.Models;

namespace RecipeApplication.Filters.Sync;

public class AddLastModifedHeaderAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is OkObjectResult result && result.Value is RecipeDetails details)
        {
            var lastModified = details.LastModified;

            context.HttpContext.Response.GetTypedHeaders().LastModified = lastModified;
        }
    }
}