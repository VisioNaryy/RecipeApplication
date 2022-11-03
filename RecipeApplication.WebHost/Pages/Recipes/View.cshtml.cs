using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Filters.Async;
using RecipeApplication.Models;
using RecipeApplication.Services;

namespace RecipeApplication.Pages.Recipes;

[PageEnsureRecipeExistsAsync]
public class View : PageModel
{
    public RecipeDetails? Recipe { get; set; } = new();
    
    private readonly IRecipesService _service;

    public View(IRecipesService service)
    {
        _service = service;
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
        return Page();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _service.DeleteRecipe(id);

        return RedirectToPage("/Index");
    }
}