using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teldat.Radios.Api.Middlewares
{
    public class DashboardMiddleware
    {
        public DashboardMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("<html><h1>Dashboard</h1></html>");
        }
    }

    public static class DashboardMiddlewareExtensions
    {
        public static IEndpointConventionBuilder MapDashboard(this IEndpointRouteBuilder endpoints, string pattern = "/dashboard")
        {
            var app = endpoints.CreateApplicationBuilder();

            var pipeline = app
                .UsePathBase(pattern)
                .UseMiddleware<DashboardMiddleware>().Build();

            return endpoints.Map(pattern, pipeline);
        }
    }
}
