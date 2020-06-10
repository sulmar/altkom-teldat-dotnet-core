using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Infrastructure.Configurations
{
    public class SoldierConfiguration : IEntityTypeConfiguration<Soldier>
    {
        public void Configure(EntityTypeBuilder<Soldier> builder)
        {
            builder
                 .Property(p => p.Unit)
                .HasMaxLength(40)
                .IsRequired();
        }
    }
}
