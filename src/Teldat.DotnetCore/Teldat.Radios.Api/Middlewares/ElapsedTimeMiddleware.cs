using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Teldat.Radios.Api.Middlewares
{
    public class ElapsedTimeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ElapsedTimeMiddleware> logger;

        public ElapsedTimeMiddleware(RequestDelegate next, ILogger<ElapsedTimeMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await next(context);
            logger.LogInformation($"{context.Request.Path} executed in {sw.ElapsedMilliseconds}ms");
            
        }
    }

    public static class ElapsedTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseElapsedTime(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ElapsedTimeMiddleware>();
        }
    }


}
