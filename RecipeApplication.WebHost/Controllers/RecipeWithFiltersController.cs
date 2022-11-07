using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApplication.Filters.Async;
using RecipeApplication.Filters.Sync;
using RecipeApplication.Models;
using RecipeApplication.Services;

namespace RecipeApplication.Controllers;

[Route("api/[controller]/[action]")]
[FeatureEnabled]
[HandleException]
[Authorize]
public class RecipeWithFiltersController : ControllerBase
{
    private readonly IRecipesService _service;

    public RecipeWithFiltersController(IRecipesService service)
    {
        _service = service;
    }
    
    [AllowAnonymous]
    [HttpGet("{id}"), ValidateModel, EnsureRecipeExistsAsync, AddLastModifedHeader]
    public async Task<IActionResult> Get(int id)
    {
        var detail = await _service.GetRecipeDetails(id);
        
        return Ok(detail);
    }
    
    [HttpPost("{id}"), ValidateModel, EnsureRecipeExistsAsync]
    public async Task<IActionResult> Edit(int id, [FromBody] RecipeToUpdate recipeToUpdate)
    {
        await _service.UpdateRecipe(recipeToUpdate);
        
        return Ok();
    }
}