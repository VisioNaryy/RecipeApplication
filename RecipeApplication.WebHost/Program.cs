using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using RecipeApplication.Core.Abstractions.Repositories;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.DataAccess;
using RecipeApplication.DataAccess.Repositories;
using RecipeApplication.DataAccess.Repositories.Sql;
using RecipeApplication.Filters.Sync;
using RecipeApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure application
var configuration = builder.Configuration;

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
services.AddScoped<IRecipesService, RecipesService>();
//services.AddScoped<FeatureEnabledAttribute>();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Add filters globally both for API and Razor Pages. Currently disabled
// services.AddControllers(options =>
// {
//     options.Filters.Add<LogResourceFilter>();
// });

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.Run();