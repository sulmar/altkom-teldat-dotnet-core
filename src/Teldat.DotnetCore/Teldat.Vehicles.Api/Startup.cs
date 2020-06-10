using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Teldat.Vehicles.Api.Services;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;
using Teldat.Vehicles.Infrastructure.DbServices;
using Teldat.Vehicles.Infrastructure.Fakers;
using Teldat.Vehicles.Infrastructure.FakeServices;

namespace Teldat.Vehicles.Api
{
   
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddXmlFile("appsettings.xml", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddHttpClient("GitHub", options =>
            {
                options.BaseAddress = new Uri("https://api.github.com");
                options.DefaultRequestHeaders.Add("Accept", "application/json");                
            });

            //services.AddSingleton<IVehicleService, FakeVehicleService>();
            //services.AddSingleton<Faker<Vehicle>, VehicleFaker>();
            //services.AddScoped<IMessageSender, SmsMessageSender>();

            // services.AddFakeServices();
            services.AddDbServices(Configuration);

            services.Configure<FakeVehicleOptions>(Configuration.GetSection("Vehicles"));

            //services.Configure<FakeVehicleOptions>
            //    (options => Options.Create(new FakeVehicleOptions { Count = 10 }));

            // dotnet add package NSwag.AspNetCore
            services.AddOpenApiDocument(options =>            {
                options.Title = "Vehicles API";
                options.DocumentName = ".NET Core 3.1 Web API";
                options.Version = "v1";
                options.Description = "Demonstrating auto-generated API documentation";
            });


            services.AddControllers().AddXmlSerializerFormatters();

            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsTesting())
            {

            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            string message = Configuration["Message"];
            int count = int.Parse(Configuration["Vehicles:Count"]);

            // generowanie dokumentacji OpenAPI
            app.UseOpenApi();
            app.UseSwaggerUi3();            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/dashboard", context => context.Response.WriteAsync("<html><h1>Dashboard</h1></html>"));
                endpoints.MapControllers();
            });
        }

        
    }

   

    public static class IWebHostEnvironmentExtensions
    {
        public static bool IsTesting(this IWebHostEnvironment env) => env.EnvironmentName == "Testing";
    }

}
