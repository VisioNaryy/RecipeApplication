using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

    public async Task DeleteRecipe(Recipe recipe)
    {
        recipe.IsDeleted = true;

        await _context.SaveChangesAsync();
    }
}