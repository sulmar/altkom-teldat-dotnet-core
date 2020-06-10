using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Teldat.Vehicles.Infrastructure.DbServices;

namespace Teldat.Vehicles.Api
{

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDbContext<VehiclesContext>().Run();

            /* 
            IHost host = CreateHostBuilder(args).Build();

            host.MigrateDbContext<VehiclesContext>();

            // await host.MigrateDbContext<VehiclesContext>();

            var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<VehiclesContext>();
            context.Database.EnsureCreated();
            //context.Database.Migrate();
                       
            host.Run();

            */
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}
