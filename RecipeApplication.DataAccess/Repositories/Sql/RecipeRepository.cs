using Microsoft.EntityFrameworkCore;
using RecipeApplication.Core.Domain.Models;

namespace RecipeApplication.DataAccess.Repositories.Sql;

public class RecipeRepository : SqlRepository<Recipe>, IRecipeRepository
{
    private readonly AppDbContext _context;

    public RecipeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Recipe?> GetExistingRecipe(int id)
    {
        return await _context.Recipes.Include(x => x.Ingredients).SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
    }

    public Task DeleteRecipe(Recipe recipe)
    {
        return Task.FromResult(recipe.IsDeleted = true);
    }

    public async Task<IEnumerable<Recipe>> GetUserRecipes(string userId, int numberOfRecipes)
    {
        return await _context.Recipes
            .Where(x => x.CreatedById == userId && !x.IsDeleted)
            .OrderBy(x => x.LastModified)
            .Take(numberOfRecipes)
            .ToListAsync();
    }
}