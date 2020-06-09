using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Teldat.Radios.Api.Middlewares
{
    public class WelcomeMessageMiddleware
    {
        public WelcomeMessageMiddleware(RequestDelegate next) { }
        
        public Task InvokeAsync(HttpContext context)
        {
            return context.Response.WriteAsync("Hello Radio!");
        }
    }

    public static class WelcomeMessageMiddlewareExtensions
    {
        public static IApplicationBuilder UseWelcomeMessage(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WelcomeMessageMiddleware>();
        }
    }

}
