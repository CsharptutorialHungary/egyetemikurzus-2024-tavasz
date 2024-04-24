using CommunitySite.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Extensions
{
    public static class AppInitExtension
    {
        public static async Task MigrateDatabases(this IServiceProvider provider)
        {
            using (var serviceScope = provider.CreateScope())
            {
                var dbcx = await serviceScope!.ServiceProvider.GetService<IDbContextFactory<ModelContext>>()!.CreateDbContextAsync();

                dbcx?.Database.Migrate();
            }
        }
    }
}
