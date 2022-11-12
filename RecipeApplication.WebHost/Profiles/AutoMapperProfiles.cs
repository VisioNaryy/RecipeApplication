using AutoMapper;
using RecipeApplication.Core.Domain.Models;
using RecipeApplication.Models;

namespace RecipeApplication.Profiles;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Recipes mapping
        CreateMap<Recipe, RecipeDetails>();

        CreateMap<RecipeToCreate, Recipe>()
            .ForMember(dest => dest.TimeToCook, opt => opt.MapFrom(src => new TimeSpan(src.TimeToCookHrs, src.TimeToCookMins, 0)))
            .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));;

        CreateMap<Recipe, RecipeToUpdate>()
            .ForMember(dest => dest.TimeToCookHrs, opt => opt.MapFrom(src => src.TimeToCook.Hours))
            .ForMember(dest => dest.TimeToCookMins, opt => opt.MapFrom(src => src.TimeToCook.Minutes));
        
        CreateMap<Recipe, RecipeSummary>()
            .ForMember(dest => dest.TimeToCook, opt => opt.MapFrom(src => $"{src.TimeToCook.TotalMinutes}mins"));
        
        CreateMap<RecipeToUpdate, Recipe>()
            .ForMember(dest => dest.TimeToCook, opt => opt.MapFrom(src => new TimeSpan(src.TimeToCookHrs, src.TimeToCookMins, 0)))
            .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));

        CreateMap<Recipe, RecipeSummary>()
            .ForMember(dest => dest.TimeToCook, opt => opt.MapFrom(src => src.TimeToCook.ToString()));
        
        // Ingredients mapping
        CreateMap<IngredientToCreate, Ingredient>();
        
        CreateMap<Ingredient, IngredientDetails>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => $"{src.Quantity} {src.Unit}"));
    }
}