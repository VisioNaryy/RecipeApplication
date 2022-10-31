using Microsoft.EntityFrameworkCore;
using RecipeApplication.Core.Domain.Models;

namespace RecipeApplication.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}