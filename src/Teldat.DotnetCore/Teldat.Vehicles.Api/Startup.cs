using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;
using Teldat.Vehicles.Infrastructure.Fakers;
using Teldat.Vehicles.Infrastructure.FakeServices;

namespace Teldat.Vehicles.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IVehicleService, FakeVehicleService>();
            services.AddSingleton<Faker<Vehicle>, VehicleFaker>();

            // dotnet add package NSwag.AspNetCore
            services.AddOpenApiDocument(options =>            {
                options.Title = "Vehicles API";
                options.DocumentName = ".NET Core 3.1 Web API";
                options.Version = "v1";
                options.Description = "Demonstrating auto-generated API documentation";
            });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // generowanie dokumentacji OpenAPI
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
