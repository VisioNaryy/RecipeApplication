using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Filters.Async;
using RecipeApplication.Models;
using RecipeApplication.Services;

namespace RecipeApplication.Pages.Recipes;

[PageEnsureRecipeExistsAsync]
public class View : PageModel
{
    private readonly IRecipesService _service;
    private readonly IAuthorizationService _authService;
    public RecipeDetails? Recipe { get; set; } = new();
    public bool CanEditRecipe { get; set; }

    public View(IRecipesService service, IAuthorizationService authService)
    {
        _service = service;
        _authService = authService;
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Recipe = await _service.GetRecipeDetails(id);
        
        if (Recipe is null)
        {
            // If id is not for a valid Recipe, generate a 404 error page
            // TODO: Add status code pages middleware to show friendly 404 page
            return NotFound();
        }
        
        var authResult = await _authService.AuthorizeAsync(User, Recipe, "IsRecipeOwner");
        
        CanEditRecipe = authResult.Succeeded;
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _service.DeleteRecipe(id);

        return RedirectToPage("/Index");
    }
}