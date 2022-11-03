using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RecipeApplication.Filters.Sync;

// This attribute is ResourceFilter
public class FeatureEnabledFilter : IResourceFilter
{
    private readonly IConfiguration _configuration;
    public bool IsEnabled { get; set; }

    public FeatureEnabledFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        if (!_configuration.GetValue<bool>("FilterValues:IsEnabled"))
        {
            context.Result = new BadRequestResult();
        }
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        
    }
}

public class FeatureEnabledAttribute : TypeFilterAttribute
{
    public FeatureEnabledAttribute() : base(typeof(FeatureEnabledFilter))
    {
        
    }
}