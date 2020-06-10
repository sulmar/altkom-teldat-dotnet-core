using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teldat.Vehicles.Domain.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace Teldat.Vehicles.Infrastructure.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
               .Property(p => p.FirstName)
               .HasMaxLength(50)
               .IsRequired();

            builder
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .ToTable("People");

            // dotnet add package System.Text.Json
            //builder
            //    .Property(p => p.HomeAddress).HasConversion(
            //        // convert
            //        v => JsonSerializer.Serialize(v),
            //        v => JsonSerializer.Deserialize<Address>(v)
            //        // convert back
            //    );

            builder
                .Property(p => p.HomeAddress).HasConversion(
                v => JsonConvert.SerializeObject(v),                // convert object -> sql
                v => JsonConvert.DeserializeObject<Address>(v)      // convert back sql -> object
                );
        }
    }
}
