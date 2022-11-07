using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.Models;
using RecipeApplication.Services;

namespace RecipeApplication.Pages.Recipes;

[Authorize]
public class Create : PageModel
{
    private readonly IRecipesService _service;
    private readonly UserManager<ApplicationUser> _userService;
    [BindProperty] public RecipeToCreate Input { get; set; }

    public Create(IRecipesService service, UserManager<ApplicationUser> userService)
    {
        _service = service;
        _userService = userService;
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
                var user = await _userService.GetUserAsync(User);

                if (user is not null)
                {
                    Input.CreatedById = user.Id;
                }
                
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