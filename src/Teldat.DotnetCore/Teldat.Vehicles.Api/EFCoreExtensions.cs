using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Teldat.Vehicles.Api
{
    public static class EFCoreExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost host)
            where TContext : DbContext
        {
            var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<TContext>();

            context.Database.EnsureCreated();
            // await context.Database.Migrate();

            return host;
        }

    }
}
