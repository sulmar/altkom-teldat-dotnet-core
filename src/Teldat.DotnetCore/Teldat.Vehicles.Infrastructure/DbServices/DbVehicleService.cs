using Bogus.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ILogger<DbVehicleService> logger;

        public DbVehicleService(VehiclesContext context, ILogger<DbVehicleService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        private void LogState(Vehicle vehicle)=> logger.LogInformation($"{vehicle.Id} {context.Entry(vehicle).State}");

        public async Task Add(Vehicle vehicle)
        {
            // LogState(vehicle);

            // context.Attach(vehicle);

            // Strategia
            context.ChangeTracker.TrackGraph(vehicle, e =>
            {
                if (e.Entry.IsKeySet)
                {
                    e.Entry.State = EntityState.Unchanged;
                }
                else
                {
                    e.Entry.State = EntityState.Added;
                }
            });

            await context.Vehicles.AddAsync(vehicle);

            //var entities = context.ChangeTracker.Entries().ToList();

          

            var entities2 = context.ChangeTracker.Entries().ToList();

            LogState(vehicle);

            await context.SaveChangesAsync();

            LogState(vehicle);
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
            Vehicle vehicle = new Vehicle { Id = id };

            // context.Entry(vehicle).State = EntityState.Deleted;
            context.Vehicles.Remove(vehicle);

            await context.SaveChangesAsync();
        }

        public async Task Update(Vehicle vehicle)
        {            
            Vehicle vehicle1 = await Get(vehicle.Id);            

            LogState(vehicle1);

            vehicle1.IsRemoved = !vehicle1.IsRemoved;

            var isModified = context.Entry(vehicle1).Property(p => p.IsRemoved).IsModified;            

            LogState(vehicle1);
            await context.SaveChangesAsync();
            LogState(vehicle1);
        }
    }
}
