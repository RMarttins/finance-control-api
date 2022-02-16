using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ApiFinance.Web.Middlaware
{
    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IServiceCollection AddGlobalExceptionHandlerMiddleware(this IServiceCollection services) => 
            services.AddTransient<GlobalExceptionHandlerMiddleware>();

        public static void UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder app) => 
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}