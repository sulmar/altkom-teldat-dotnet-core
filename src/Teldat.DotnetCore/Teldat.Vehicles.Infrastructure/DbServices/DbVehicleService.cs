using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Infrastructure.DbServices
{
    public class DbVehicleService : IVehicleService
    {
        private readonly VehiclesContext context;

        public async Task Add(Vehicle vehicle)
        {
            await context.Vehicles.AddAsync(vehicle);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehicle>> Get()
        {
            return await context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> Get(int id)
        {
            return await context.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> Get(string vin)
        {
            return await context.Vehicles.SingleOrDefaultAsync(v => v.Vin == vin);
        }

        public async Task Remove(int id)
        {
            Vehicle vehicle = await Get(id);

            context.Vehicles.Remove(vehicle);

            await context.SaveChangesAsync();
        }

        public Task Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
