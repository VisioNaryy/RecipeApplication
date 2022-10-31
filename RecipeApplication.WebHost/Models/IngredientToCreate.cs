using System.ComponentModel.DataAnnotations;

namespace RecipeApplication.Models;

public class IngredientToCreate
{
    [Required, StringLength(100)]
    public string Name { get; set; }
    
    [Range(0, int.MaxValue)]
    public decimal Quantity { get; set; }
    
    [StringLength(20)]
    public string Unit { get; set; }
}