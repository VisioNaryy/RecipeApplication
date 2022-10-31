using AutoMapper;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.DataAccess.Repositories;
using RecipeApplication.Models;

namespace RecipeApplication.Services;

public class RecipesService : IRecipesService
{
    private readonly IRecipeRepository _recipeRepo;
    private readonly IMapper _mapper;

    public RecipesService(IRecipeRepository recipeRepo, IMapper mapper)
    {
        _recipeRepo = recipeRepo;
        _mapper = mapper;
    }
    
    public async ValueTask<int> CreateRecipeAsync(RecipeToCreate recipeToCreate)
    {
        var recipe = _mapper.Map<RecipeToCreate, Recipe>(recipeToCreate);

        await _recipeRepo.CreateRecipe(recipe);
        await _recipeRepo.SaveChangesAsync();

        return recipe.Id;
    }
    
    public async Task UpdateRecipe(RecipeToUpdate recipeToToUpdate)
    {
        var recipe = await _recipeRepo.GetByIdAsync(recipeToToUpdate.Id);
        
        if (recipe == null) { throw new Exception("Unable to find the recipe"); }
        if (recipe.IsDeleted) { throw new Exception("Unable to update a deleted recipe"); }

        _mapper.Map(recipeToToUpdate, recipe);
        
        await _recipeRepo.SaveChangesAsync();
    }
    
    public async Task DeleteRecipe(int recipeId)
    {
        var recipe = await _recipeRepo.GetByIdAsync(recipeId);
        
        if (recipe == null) { throw new Exception("Unable to find the recipe"); }

        await _recipeRepo.DeleteRecipe(recipe);
    }

    public async Task<Recipe?> GetRecipe(int recipeId)
    {
        return await _recipeRepo.GetByIdAsync(recipeId);
    }
    
    public async Task<IEnumerable<RecipeSummary>> GetRecipes()
    {
        var recipes = await _recipeRepo.GetAllAsync();

        return _mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeSummary>>(recipes);
    }
    
    public async Task<RecipeDetails?> GetRecipeDetails(int recipeId)
    {
        var recipe = await _recipeRepo.GetExistingRecipe(recipeId);
        
        return _mapper.Map<Recipe, RecipeDetails>(recipe);
    }
    
    public async Task<RecipeToUpdate?> GetRecipeForUpdate(int recipeId)
    {
        var recipe = await _recipeRepo.GetExistingRecipe(recipeId);
        
        return _mapper.Map<Recipe, RecipeToUpdate>(recipe);
    }
}