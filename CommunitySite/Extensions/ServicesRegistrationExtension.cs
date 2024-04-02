using CommunitySite.Data.Entities;
using CommunitySite.Data.ViewModels;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
