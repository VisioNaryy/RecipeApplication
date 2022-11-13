using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RecipeApplication.Authorization;
using RecipeApplication.Core.Abstractions.Repositories;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.DataAccess;
using RecipeApplication.DataAccess.Repositories;
using RecipeApplication.DataAccess.Repositories.Sql;
using RecipeApplication.Extensions;
using RecipeApplication.Handlers;
using RecipeApplication.Policies;
using RecipeApplication.Services;
using RecipeApplication.SyncDataServices;
using RecipeApplication.Validation;

var builder = WebApplication.CreateBuilder(args);

// Configure application
var configuration = builder.Configuration;

configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true,
    reloadOnChange: true);
if (builder.Environment.IsDevelopment())
{
    configuration.AddUserSecrets<Program>(optional: true, reloadOnChange: true);
}

// Add services to the container.
var services = builder.Services;

services.AddRazorPages().AddFluentValidation(options =>
{
    options.DisableDataAnnotationsValidation = true;
    options.RegisterValidatorsFromAssemblyContaining<RecipeBaseValidator>();
});
services.AddDbContext<AppDbContext>(options =>
{
    var connection = configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connection);
});
services.AddDefaultIdentity<ApplicationUser>(options => { options.SignIn.RequireConfirmedAccount = true; })
    .AddEntityFrameworkStores<AppDbContext>();
services.AddScoped<IRecipeRepository, RecipeRepository>();
services.Decorate<IRecipeRepository, CachedRecipeRepository>();
services.AddScoped<IRepository<Recipe>, SqlRepository<Recipe>>();
services.AddScoped<IRecipesService, RecipesService>();
services.AddScoped<IAuthorizationHandler, IsRecipeOwnerHandler>();
services.AddAuthorization(options =>
{
    options.AddPolicy("IsRecipeOwner",
        policyBuilder => { policyBuilder.AddRequirements(new IsRecipeOwnerRequirement()); });
});
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddTransient<ApiKeyMessageHandler>();
services.AddHttpClient<IWikipediaClient, WikipediaClient>(client =>
    {
        client.BaseAddress = new Uri(configuration.GetValue<string>("WikipediaApiAddress")!);
    })
    .AddHttpMessageHandler<ApiKeyMessageHandler>();
services.AddSingleton<IClientPolicy, ClientPolicy>();

// Use services
var app = builder.Build();

// Custom middleware
app.UseSecurityHeaders();

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