namespace RecipeApplication.Core.Domain;

public class BaseEntity
{
    public int Id { get; set; }
    
    public bool IsDeleted { get; set; }
}