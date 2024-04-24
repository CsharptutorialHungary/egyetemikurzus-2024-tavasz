using CommunitySite.Data.Entities;
using CommunitySite.Services.AdminServices;
using CommunitySite.Services.AdminViewServices;
using CommunitySite.Services.ClaimsServices;
using CommunitySite.Services.UserServices;
using CommunitySite.Services.UserViewServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System.Data;
using System.Data.Common;

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
            services.AddScoped<IDialogService, DialogService>();
            services.AddScoped<IUserViewService, UserViewService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAdminViewService, AdminViewService>();

            return services;
        }
    }
}
