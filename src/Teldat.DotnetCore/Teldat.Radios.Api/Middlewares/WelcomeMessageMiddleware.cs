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

}
