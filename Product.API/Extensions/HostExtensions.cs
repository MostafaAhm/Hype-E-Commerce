using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Persistence;

namespace Product.API.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> MigrateDatabaseAsync<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<AppContextSeed>>();
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    await context.Database.MigrateAsync();
                    await AppContextSeed.SeedAsync(context, logger);

                }
                catch (Exception ex)
                {

                    logger.LogError(ex, "An error occured during migration");
                }
            }
            return host;
        }
    }
}
