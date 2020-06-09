using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teldat.Radios.Api.Middlewares
{
    public static class RadiosMiddlewareExtensions
    {
        public static IApplicationBuilder UseRadios(this IApplicationBuilder app)
        {
            app.Map("/radios", node =>
            {
                node.Map("/offline",
                    options => options.Run(context => context.Response.WriteAsync("offline radios")));

                node.Map("/online",
                    options => options.Run(context => context.Response.WriteAsync("online radios")));

                node.MapWhen(context => context.Request.Method == "POST",
                    options => options.Run(context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status201Created;
                        return context.Response.WriteAsync("Created radio");
                    }));

                node.Map(string.Empty,
                    options => options.Run(context => context.Response.WriteAsync("All Radios")));

                
            });

            return app;
        }
    }
}
