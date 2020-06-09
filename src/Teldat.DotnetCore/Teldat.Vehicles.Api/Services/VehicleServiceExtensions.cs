using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;
using Teldat.Vehicles.Infrastructure.Fakers;
using Teldat.Vehicles.Infrastructure.FakeServices;

namespace Teldat.Vehicles.Api.Services
{
    public static class VehicleServiceExtensions
    {
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
