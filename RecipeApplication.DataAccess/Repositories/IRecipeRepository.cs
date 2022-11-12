using RecipeApplication.Core.Abstractions.Repositories;
using RecipeApplication.Core.Domain.Models;

namespace RecipeApplication.DataAccess.Repositories;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe?> GetExistingRecipe(int id);

    Task DeleteRecipe(Recipe recipe);

    Task<IEnumerable<Recipe>> GetUserRecipes(string userId, int numberOfRecipes);
}