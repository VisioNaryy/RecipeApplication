using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;
using RecipeApplication.Services;

namespace RecipeApplication.Pages.Recipes;

public class Create : PageModel
{
    [BindProperty]
    public RecipeToCreate Input { get; set; }
    
    private readonly IRecipesService _service;

    public Create(IRecipesService service)
    {
        _service = service;
    }
    
    public void OnGet()
    {
        Input = new RecipeToCreate();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (ModelState.IsValid)
            {
                var id = await _service.CreateRecipeAsync(Input);
                return RedirectToPage("View", new { id = id });
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

        return Page();
    }
}