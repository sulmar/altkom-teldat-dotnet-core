using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Infrastructure.DbServices
{

    // dotnet add package Microsoft.EntityFrameworkCore
    public class VehiclesContext : DbContext
    {
        public VehiclesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Civil> Civils { get; set; }
        public DbSet<Soldier> Soldiers { get; set; }

    }
}
