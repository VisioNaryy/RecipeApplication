using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Filters.Async;
using RecipeApplication.Models;
using RecipeApplication.Services;

namespace RecipeApplication.Pages.Recipes;

[PageEnsureRecipeExistsAsync]
public class Edit : PageModel
{
    private readonly IRecipesService _service;
    private readonly IAuthorizationService _authService;
    [BindProperty] public RecipeToUpdate? Input { get; set; } = new();

    public Edit(IRecipesService service, IAuthorizationService authService)
    {
        _service = service;
        _authService = authService;
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Input = await _service.GetRecipeForUpdate(id);

        if (Input is null)
        {
            // If id is not for a valid Recipe, generate a 404 error page
            // TODO: Add status code pages middleware to show friendly 404 page
            return NotFound();
        }

        var authResult = await _authService.AuthorizeAsync(User, Input, "IsRecipeOwner");

        if (!authResult.Succeeded)
        {
            return new ForbidResult();
        }
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateRecipe(Input);

                return RedirectToPage("View", new {id = Input.Id});
            }
        }
        catch (Exception ex)
        {
            // TODO: Log error
            // Add a model-level error by using an empty string key
            ModelState.AddModelError(
                string.Empty,
                "An error occured saving the recipe"
            );
        }
        
        //If we got to here, something went wrong
        return Page();
    }
}