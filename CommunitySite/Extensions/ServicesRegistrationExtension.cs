using CommunitySite.Data.Entities;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Extensions
{
    public static class ServicesRegistrationExtension
    {
        public static IServiceCollection RegisterDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConns = configuration.GetConnectionString("default");
            services.AddPooledDbContextFactory<ModelContext>(options => options
                .UseOracle(defaultConns)
            );

            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<IClaimsTransformation, ClaimsTransformation>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
