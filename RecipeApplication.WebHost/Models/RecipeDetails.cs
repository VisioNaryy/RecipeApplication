namespace RecipeApplication.Models;

public class RecipeDetails : RecipeBase
{
    public int Id { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public IEnumerable<IngredientDetails> Ingredients { get; set; }
}