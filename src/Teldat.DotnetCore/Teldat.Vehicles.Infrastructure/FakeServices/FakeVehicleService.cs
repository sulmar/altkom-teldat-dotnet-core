using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Infrastructure.FakeServices
{
    public class FakeVehicleService : IVehicleService
    {
        private readonly ICollection<Vehicle> vehicles;

        public FakeVehicleService(Faker<Vehicle> faker)
        {
            vehicles = faker.Generate(100);
        }

        public Task Add(Vehicle vehicle)
        {
            vehicles.Add(vehicle);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Vehicle>> Get()
        {
            return Task.FromResult<IEnumerable<Vehicle>>(vehicles);
        }

        public Task<Vehicle> Get(int id)
        {
            return Task.FromResult(vehicles.SingleOrDefault(v => v.Id == id));
        }

        public async Task Remove(int id)
        {
            var vehicle = await Get(id);
            vehicles.Remove(vehicle);
        }

        public async Task Update(Vehicle vehicle)
        {
            await Remove(vehicle.Id);
            await Add(vehicle);
        }
    }
}
