using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Infrastructure.DbServices
{
    public class DbSoldiersService : ISoldierService
    {
        private readonly VehiclesContext context;

        public DbSoldiersService(VehiclesContext context)
        {
            this.context = context;
        }

        public async Task Add(Soldier entity)
        {
            await context.AddAsync(entity);

            await context.SaveChangesAsync();
        }

        public Task<IEnumerable<Soldier>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Soldier> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Soldier entity)
        {
            throw new NotImplementedException();
        }
    }

    public class DbVehicleService : IVehicleService
    {
        private readonly VehiclesContext context;

        public DbVehicleService(VehiclesContext context)
        {
            this.context = context;
        }

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
