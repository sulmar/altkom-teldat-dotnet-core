using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Schema;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.ConsoleClient
{
    class Program
    {
        // dotnet add package Microsoft.Extensions.DependencyInjection
        static async Task Main(string[] args)
        {
            Console.WriteLine("Rest API Console Client");

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IVehicleService, ServiceStackVehicleService>();                

            var serviceProvider = services.BuildServiceProvider();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");

            // IVehicleService vehicleService = new ApiVehicleService(client);
            // IVehicleService vehicleService = new ServiceStackVehicleService();

            IVehicleService vehicleService = serviceProvider.GetRequiredService<IVehicleService>();

            IEnumerable <Vehicle> vehicles = await vehicleService.Get();

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"{vehicle.Model} {vehicle.ProductionYear}");
            }

            Vehicle vehicle1 = await vehicleService.Get(1);

            Vehicle vehicle2 = new Vehicle
            {
                Model = "Syrena",
                ProductionYear = 1974,
            };

            await vehicleService.Add(vehicle2);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

    // ServiceStack.HttpClient

    public class ServiceStackVehicleService : IVehicleService
    {
        private readonly string baseUri;


        public ServiceStackVehicleService()
            : this("http://localhost:5000")
        {

        }

        public ServiceStackVehicleService(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public async Task Add(Vehicle vehicle)
        {
            string url = $"{baseUri}/api/vehicles";

            await url.PostJsonToUrlAsync(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> Get()
        {
            string url = $"{baseUri}/api/vehicles";

            string json = await url.GetJsonFromUrlAsync();

            return json.FromJson<IEnumerable<Vehicle>>();
        }

        public async Task<Vehicle> Get(int id)
        {
            string url = $"{baseUri}/api/vehicles/{id}";

            string json = await url.GetJsonFromUrlAsync();

            return json.FromJson<Vehicle>();
        }

        public async Task<Vehicle> Get(string vin)
        {
            string url = $"{baseUri}/api/vehicles/{vin}";

            string json = await url.GetJsonFromUrlAsync();

            return json.FromJson<Vehicle>();
        }

        public async Task Remove(int id)
        {
            string url = $"{baseUri}/api/vehicles/{id}";

            await url.DeleteFromUrlAsync(url);
        }

        public Task Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }

    public class ApiVehicleService : IVehicleService
    {
        private readonly HttpClient client;

        public ApiVehicleService(HttpClient client)
        {
            this.client = client;
        }

        public async Task Add(Vehicle vehicle)
        {
            var json = JsonConvert.SerializeObject(vehicle);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/vehicles", content);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            vehicle = JsonConvert.DeserializeObject<Vehicle>(jsonResponse);

        }

        // dotnet add package Newtonsoft.Json
        public async Task<IEnumerable<Vehicle>> Get()
        {
            string json = await client.GetStringAsync("api/vehicles");

            var vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(json);

            return vehicles;
        }

        public async Task<Vehicle> Get(int id)
        {
            string json = await client.GetStringAsync($"api/vehicles/{id}");

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);

            return vehicle;
        }

        public Task<Vehicle> Get(string vin)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }

}
