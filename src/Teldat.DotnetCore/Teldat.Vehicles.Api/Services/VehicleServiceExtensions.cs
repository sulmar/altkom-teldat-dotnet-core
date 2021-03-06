﻿using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;
using Teldat.Vehicles.Infrastructure.DbServices;
using Teldat.Vehicles.Infrastructure.Fakers;
using Teldat.Vehicles.Infrastructure.FakeServices;

namespace Teldat.Vehicles.Api.Services
{
    public static class VehicleServiceExtensions
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageSender, SmsMessageSender>();

            services.AddScoped<IVehicleService, DbVehicleService>();
            services.AddScoped<ISoldierService, DbSoldiersService>();

            string connectionString = configuration.GetConnectionString("VehiclesConnection");

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            // services.AddDbContext<VehiclesContext>(options => options.UseSqlServer(connectionString));

            services.AddDbContextPool<VehiclesContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddFakeServices(this IServiceCollection services)
        {
            services.AddFakeVehicleServices();
            services.AddScoped<IMessageSender, SmsMessageSender>();

            return services;
        }

        public static IServiceCollection AddFakeVehicleServices(this IServiceCollection services)
        {
            services.AddSingleton<IVehicleService, FakeVehicleService>();
            services.AddSingleton<Faker<Vehicle>, VehicleFaker>();

            return services;
        }
    }
}
