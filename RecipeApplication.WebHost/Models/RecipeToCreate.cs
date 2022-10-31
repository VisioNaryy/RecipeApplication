namespace RecipeApplication.Models;

public class RecipeToCreate : RecipeBase
{
    public IList<IngredientToCreate> Ingredients { get; set; } = new List<IngredientToCreate>();
}