using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.Models;

namespace RecipeApplication.Authorization;

public class IsRecipeOwnerHandler : AuthorizationHandler<IsRecipeOwnerRequirement, RecipeBase>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IsRecipeOwnerHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRecipeOwnerRequirement requirement, RecipeBase resource)
    {
        var user = await _userManager.GetUserAsync(context.User);

        if (user is null)
            return;
        
        if (resource.CreatedById == user.Id)
        {
            context.Succeed(requirement);
        }
    }
}