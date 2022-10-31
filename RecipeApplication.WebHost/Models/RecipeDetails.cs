namespace RecipeApplication.Models;

public class RecipeDetails
{
    public int Id { get; set; }
    
    public string? Name { get; set; }

    public string? Method { get; set; }
    
    public DateTimeOffset LastModified { get; set; }

    public IEnumerable<Item> Ingredients { get; set; }
}