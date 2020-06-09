using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Teldat.Radios.Api.Middlewares;

namespace Teldat.Radios.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Podpinanie warstw poœrednich za pomoc¹ metod Use() i Run()

            // Logger
            //app.Use(async (context, next) =>
            //   {
            //       Trace.WriteLine($"{context.Request.Method} {context.Request.Path}");

            //       await next();

            //       Trace.WriteLine($"{context.Response.StatusCode}");
            //   });

            // Authorization
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.ContainsKey("Authorization"))
            //    {
            //        await next();
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    }
            //});

            // Welcome Message
            //app.Run(context => context.Response.WriteAsync("Hello Radio!"));

            // Elapsed Time
            //app.Use(async (context, next) =>
            //{
            //    var sw = new Stopwatch();
            //    sw.Start();

            //    await next();

            //    ILogger logger = context.RequestServices.GetRequiredService<ILogger>();

            //    logger.LogInformation($"{context.Request.Path} executed in {sw.ElapsedMilliseconds}ms");
            //});

            #endregion

            #region Podpinanie warstw poœrednich za pomoc¹ metody UseMiddleware()
            // app.UseMiddleware<ElapsedTimeMiddleware>();
            // app.UseMiddleware<LoggerMiddleware>();
            // app.UseMiddleware<AuthorizationMiddleware>();
            // app.UseMiddleware<WelcomeMessageMiddleware>();
            #endregion

            #region Podpinanie warstw poœrednich za pomoc¹ metod rozszerzaj¹ych
            app.UseElapsedTime();
            app.UseLogger();
            // app.UseSecurity();

            #endregion


            #region Mapy

            // app.Map("/dashboard", options => options.Run(context => context.Response.WriteAsync("Dashboard")));

            //app.Map("/radios", node =>
            //{              
            //    node.Map("/offline",
            //        options => options.Run(context => context.Response.WriteAsync("offline radios")));

            //    node.Map("/online",
            //        options => options.Run(context => context.Response.WriteAsync("online radios")));

            //    node.Map(string.Empty,
            //        options => options.Run(context => context.Response.WriteAsync("All Radios")));
            //});

            // app.UseRadios();

            #endregion

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDashboard("/dashboard");

                endpoints.MapGet("/radios", async context => await context.Response.WriteAsync("All Radios"));
                endpoints.MapGet("/radios/offline", async context => await context.Response.WriteAsync("All offline"));
                endpoints.MapPost("/radios", async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status201Created;
                    await context.Response.WriteAsync("Created radio");
                });
                endpoints.MapGet("/radios/{id:int}", async context =>
                {
                    if (context.Request.RouteValues.TryGetValue("id", out object id))
                    {
                        await context.Response.WriteAsync($"Radio {id}");
                    }
                });
            });




            // Endpoints



            app.UseWelcomeMessage();

        }
    }
}
