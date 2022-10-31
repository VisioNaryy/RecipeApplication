using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;
using RecipeApplication.Services;

namespace RecipeApplication.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IRecipesService _service;

    public IEnumerable<RecipeSummary> Recipes { get; set; } = new List<RecipeSummary>();

    public IndexModel(ILogger<IndexModel> logger, IRecipesService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task OnGetAsync()
    {
        Recipes = await _service.GetRecipes();
    }
}