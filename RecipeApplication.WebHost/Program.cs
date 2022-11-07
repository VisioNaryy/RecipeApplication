using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RecipeApplication.Authorization;
using RecipeApplication.Core.Abstractions.Repositories;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.DataAccess;
using RecipeApplication.DataAccess.Repositories;
using RecipeApplication.DataAccess.Repositories.Sql;
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
services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<AppDbContext>();

services.AddScoped<IRecipeRepository, RecipeRepository>();
services.AddScoped<IRepository<Recipe>, SqlRepository<Recipe>>();
services.AddScoped<IRecipesService, RecipesService>();
services.AddScoped<IAuthorizationHandler, IsRecipeOwnerHandler>();
services.AddAuthorization(options =>
{
    options.AddPolicy("IsRecipeOwner", policyBuilder =>
    {
        policyBuilder.AddRequirements(new IsRecipeOwnerRequirement());
    });
});
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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.Run();