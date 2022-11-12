using RecipeApplication.Core.Domain.Models;
using RecipeApplication.Models;

namespace RecipeApplication.Services;

public interface IRecipesService
{
    ValueTask<int> CreateRecipeAsync(RecipeToCreate recipeToCreate);
    Task UpdateRecipe(RecipeToUpdate recipeToToUpdate);
    Task DeleteRecipe(int recipeId);
    Task<Recipe?> GetRecipe(int recipeId);
    Task<IEnumerable<RecipeSummary>> GetRecipes();
    Task<RecipeDetails?> GetRecipeDetails(int recipeId);
    Task<RecipeToUpdate?> GetRecipeForUpdate(int recipeId);
    Task<IEnumerable<RecipeSummary>> GetUserRecipeSummary(string userId, int numberOfRecipes);
}