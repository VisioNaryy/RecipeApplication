namespace RecipeApplication.Core.Domain.Models;

public class Recipe : BaseEntity
{
    public string? Name { get; set; }
    
    public TimeSpan TimeToCook { get; set; }

    public string? Method { get; set; }

    public bool IsVegetarian { get; set; }

    public bool IsVegan { get; set; }

    public string TestField { get; set; }
    
    public DateTimeOffset LastModified { get; set; }

    public IEnumerable<Ingredient>? Ingredients { get; set; }
}