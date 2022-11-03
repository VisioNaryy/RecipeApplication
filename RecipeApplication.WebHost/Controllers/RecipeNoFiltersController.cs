using Microsoft.AspNetCore.Mvc;
using RecipeApplication.DataAccess.Repositories;
using RecipeApplication.Filters.Sync;
using RecipeApplication.Models;
using RecipeApplication.Services;

namespace RecipeApplication.Controllers;

[Route("api/[controller]/[action]")]
public class RecipeNoFiltersController : ControllerBase
{
    public bool IsEnabled { get; set; } = true;
    private readonly IRecipesService _service;
    private readonly IRecipeRepository _recipeRepo;

    public RecipeNoFiltersController(IRecipesService service, IRecipeRepository recipeRepo)
    {
        _service = service;
        _recipeRepo = recipeRepo;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (!IsEnabled)
            return BadRequest();

        try
        {
            if (!await _recipeRepo.DoesEntityExist(id))
            {
                return NotFound();
            }

            var recipeDetails = await _service.GetRecipeDetails(id);

            Response.GetTypedHeaders().LastModified = recipeDetails?.LastModified;

            return Ok(recipeDetails);
        }
        catch (Exception ex)
        {
            return GetErrorResponse(ex);
        }
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] RecipeToUpdate recipeToUpdate)
    {
        if (!IsEnabled)
            return BadRequest();

        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _recipeRepo.DoesEntityExist(id))
            {
                return NotFound();
            }

            await _service.UpdateRecipe(recipeToUpdate);

            return Ok();
        }
        catch (Exception ex)
        {
            return GetErrorResponse(ex);
        }
    }

    private static IActionResult GetErrorResponse(Exception ex)
    {
        var error = new ProblemDetails
        {
            Title = "An error occured",
            Detail = ex.Message,
            Status = 500,
            Type = "https://httpstatuses.com/500"
        };

        return new ObjectResult(error)
        {
            StatusCode = 500
        };
    }
}