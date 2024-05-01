using CommunitySite.Data.Context;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace CommunitySite.Extensions
{
    public static class AppInitExtension
    {
        public static async Task MigrateDatabases(this IServiceProvider provider)
        {
            try
            {
                using (var serviceScope = provider.CreateScope())
                {
                    var dbcx = await serviceScope!.ServiceProvider.GetService<IDbContextFactory<CommunitySiteContext>>()!.CreateDbContextAsync();

                    dbcx?.Database.Migrate();
                }
            }
            catch (OracleException ex)
            {
                //TODO
            }
        }
    }
}
