using FoodDeliveryApplication.Service;

namespace BlogApi.Middleware;

public static class MiddlewareExtensions
{
    public static void UseExceptionHandlingMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}