using Microsoft.Extensions.Caching.Memory;
using RecipeApplication.Core.Domain.Models;

namespace RecipeApplication.DataAccess.Repositories.Sql;

public class CachedRecipeRepository : IRecipeRepository
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedRecipeRepository(IRecipeRepository recipeRepository, IMemoryCache memoryCache)
    {
        _recipeRepository = recipeRepository;
        _memoryCache = memoryCache;
    }
    public async Task<Recipe?> GetByIdAsync(int id)
    {
        var key = $"member-{id}";
        
        return await _memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
        
            return _recipeRepository.GetByIdAsync(id);
        });
    }

    public Task<IEnumerable<Recipe>> GetAllAsync()
    {
        return _recipeRepository.GetAllAsync();
    }

    public Task AddAsync(Recipe entity)
    {
        return _recipeRepository.AddAsync(entity);
    }

    public Task RemoveAsync(Recipe entity)
    {
        return _recipeRepository.RemoveAsync(entity);
    }

    public Task<bool> DoesEntityExist(int id)
    {
        return _recipeRepository.DoesEntityExist(id);
    }

    public Task<int> SaveChangesAsync()
    {
        return _recipeRepository.SaveChangesAsync();
    }

    public async Task<Recipe?> GetExistingRecipe(int id)
    {
        var key = $"member-{id}";
        
        return await _memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return _recipeRepository.GetExistingRecipe(id);
        });
    }

    public Task DeleteRecipe(Recipe recipe)
    {
        return _recipeRepository.DeleteRecipe(recipe);
    }

    public Task<IEnumerable<Recipe>> GetUserRecipes(string userId, int numberOfRecipes)
    {
        return _recipeRepository.GetUserRecipes(userId, numberOfRecipes);
    }
}