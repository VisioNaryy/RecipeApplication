using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.Services;

namespace RecipeApplication.CustomViewComponents;

public class MyRecipesViewComponent : ViewComponent
{
    private readonly IRecipesService _recipesService;
    private readonly UserManager<ApplicationUser> _userManager;

    public MyRecipesViewComponent(IRecipesService recipesService, UserManager<ApplicationUser> userManager)
    {
        _recipesService = recipesService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(int numberOfRecipes = 4)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return View("Unauthenticated");
        }
        
        var userId = _userManager.GetUserId(HttpContext.User);

        var recipesSummary = await _recipesService.GetUserRecipeSummary(userId, numberOfRecipes);

        return View(recipesSummary);
    }
}