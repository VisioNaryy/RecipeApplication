using AutoMapper;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.DataAccess;
using RecipeApplication.DataAccess.Repositories;
using RecipeApplication.Models;

namespace RecipeApplication.Services;

public class RecipesService : IRecipesService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;

    public RecipesService(IRecipeRepository recipeRepository, IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
    }
    
    public async ValueTask<int> CreateRecipeAsync(RecipeToCreate recipeToCreate)
    {
        var recipe = _mapper.Map<RecipeToCreate, Recipe>(recipeToCreate);
        
        await _recipeRepository.AddAsync(recipe!);
        
        await _recipeRepository.SaveChangesAsync();

        return recipe.Id;
    }
    
    public async Task UpdateRecipe(RecipeToUpdate recipeToToUpdate)
    {
        var recipe = await _recipeRepository.GetByIdAsync(recipeToToUpdate.Id);
        
        if (recipe == null) { throw new Exception("Unable to find the recipe"); }
        if (recipe.IsDeleted) { throw new Exception("Unable to update a deleted recipe"); }

        _mapper.Map(recipeToToUpdate, recipe);

        await _recipeRepository.SaveChangesAsync();
    }
    
    public async Task DeleteRecipe(int recipeId)
    {
        var recipe = await _recipeRepository.GetByIdAsync(recipeId);
        
        if (recipe == null) { throw new Exception("Unable to find the recipe"); }

        await _recipeRepository.DeleteRecipe(recipe);

        await _recipeRepository.SaveChangesAsync();
    }

    public async Task<Recipe?> GetRecipe(int recipeId)
    {
        return await _recipeRepository.GetByIdAsync(recipeId);
    }
    
    public async Task<IEnumerable<RecipeSummary>> GetRecipes()
    {
        var recipes = await _recipeRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeSummary>>(recipes);
    }
    
    public async Task<RecipeDetails?> GetRecipeDetails(int recipeId)
    {
        var recipe = await _recipeRepository.GetExistingRecipe(recipeId);

        return _mapper.Map<Recipe, RecipeDetails>(recipe!);
    }
    
    public async Task<RecipeToUpdate?> GetRecipeForUpdate(int recipeId)
    {
        var recipe = await _recipeRepository.GetExistingRecipe(recipeId);

        return _mapper.Map<Recipe, RecipeToUpdate>(recipe!);
    }

    public async Task<IEnumerable<RecipeSummary>> GetUserRecipeSummary(string userId, int numberOfRecipes)
    {
        var recipes = await _recipeRepository.GetUserRecipes(userId, numberOfRecipes);

        return _mapper.Map<IEnumerable<RecipeSummary>>(recipes);
    }
}