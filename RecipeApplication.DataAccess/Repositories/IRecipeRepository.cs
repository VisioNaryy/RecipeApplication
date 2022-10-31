using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipeApplication.Core.Abstractions.Repositories;
using RecipeApplication.Core.Domain.Models;

namespace RecipeApplication.DataAccess.Repositories;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe> GetExistingRecipe(int id);
    
    Task CreateRecipe(Recipe recipeCreateDto);

    Task DeleteRecipe(Recipe recipe);
}