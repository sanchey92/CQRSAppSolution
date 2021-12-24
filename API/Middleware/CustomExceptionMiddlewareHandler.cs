using Microsoft.AspNetCore.Builder;

namespace API.Middleware
{
    public static class CustomExceptionMiddlewareHandler
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddlewareHandler>();
        }
    }
}