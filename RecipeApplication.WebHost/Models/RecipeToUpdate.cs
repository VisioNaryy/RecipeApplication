using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipeApplication.Models;

public class RecipeToUpdate : RecipeBase
{
    public int Id { get; set; }
}