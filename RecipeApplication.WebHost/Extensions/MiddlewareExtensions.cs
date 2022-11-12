using RecipeApplication.Middleware;

namespace RecipeApplication.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<HeadersMiddleware>();
    }
}