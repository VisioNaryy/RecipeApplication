using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RecipeApplication.Validation;

namespace RecipeApplication.Models;

public class RecipeBase
{
    public string? Name { get; set; }
    
    [DisplayName("Time to cook (hrs)")]
    public int TimeToCookHrs { get; set; }
    
    [DisplayName("Time to cook (mins)")]
    public int TimeToCookMins { get; set; }
    
    public string? Method { get; set; }
    
    [DisplayName("Vegetarian?")]
    public bool IsVegetarian { get; set; }
    
    [DisplayName("Vegan?")]
    public bool IsVegan { get; set; }
    
    public string? CreatedById { get; set; }
}