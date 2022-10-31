using Microsoft.EntityFrameworkCore;
using RecipeApplication.Core.Abstractions.Repositories;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.DataAccess;
using RecipeApplication.DataAccess.Repositories;
using RecipeApplication.DataAccess.Repositories.Sql;
using RecipeApplication.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

//configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
if (builder.Environment.IsDevelopment())
{
    configuration.AddUserSecrets<Program>(optional: true, reloadOnChange: true);
}

// Add services to the container.
var services = builder.Services;

services.AddRazorPages();
services.AddDbContext<AppDbContext>(options =>
{
    var connection = configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connection);
});
services.AddScoped<IRecipeRepository, RecipeRepository>();
services.AddScoped<IRepository<Recipe>, SqlRepository<Recipe>>();
services.AddScoped<IRepository<Ingredient>, SqlRepository<Ingredient>>();
services.AddScoped<IRecipesService, RecipesService>();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Use services
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();