using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Infrastructure.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder
               .Property(p => p.Vin).HasMaxLength(50).IsRequired().IsUnicode(false);

            builder
                .HasIndex(p => p.Vin)
                .IsUnique();

            builder
               .Property(p => p.Color)
               .HasMaxLength(50);

            // filtr globalny
            builder
                .HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
