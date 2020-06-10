using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Teldat.Vehicles.Domain.Models;
using Teldat.Vehicles.Infrastructure.Configurations;

namespace Teldat.Vehicles.Infrastructure.DbServices
{

    // dotnet add package Microsoft.EntityFrameworkCore
    // dotnet add package Microsoft.EntityFrameworkCore.Relational
    public class VehiclesContext : DbContext
    {
        public VehiclesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Civil> Civils { get; set; }
        public DbSet<Soldier> Soldiers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("vehicles");

            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new SoldierConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
