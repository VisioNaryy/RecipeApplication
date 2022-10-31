namespace RecipeApplication.Core.Domain.Models;

public class Ingredient : BaseEntity
{
    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; }
    
    public string? Name { get; set; }
    
    public decimal Quantity { get; set; }
    
    public string? Unit { get; set; }
}