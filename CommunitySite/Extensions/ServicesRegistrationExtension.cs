using CommunitySite.Data.Context;
using CommunitySite.Services.AdminServices;
using CommunitySite.Services.AdminViewServices;
using CommunitySite.Services.ClaimsServices;
using CommunitySite.Services.FriendServices;
using CommunitySite.Services.GroupPostUploadServices;
using CommunitySite.Services.GroupServices;
using CommunitySite.Services.GroupViewServices;
using CommunitySite.Services.ImageServices;
using CommunitySite.Services.PostServices;
using CommunitySite.Services.PostViewService;
using CommunitySite.Services.UserServices;
using CommunitySite.Services.UserViewServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace CommunitySite.Extensions
{
    public static class ServicesRegistrationExtension
    {
        /// <summary>
        ///     Regisztrálja az Oracle adatbázist a szolgáltatások közé
        /// </summary>
        /// <param name="services">ASP.NET szervíz osztályok kollekciója</param>
        /// <param name="configuration">appsettings.json</param>
        /// <returns>ASP.NET szervíz osztályok kollekciója</returns>
        public static IServiceCollection RegisterDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConns = configuration.GetConnectionString("default");
            services.AddPooledDbContextFactory<CommunitySiteContext>(options => options
                .UseOracle(defaultConns)
                .EnableSensitiveDataLogging()
            );

            return services;
        }

        /// <summary>
        ///     Regisztrálja a saját szervíz osztályokat
        /// </summary>
        /// <param name="services">ASP.NET szervíz osztályok kollekciója</param>
        /// <returns>ASP.NET szervíz osztályok kollekciója</returns>
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<IClaimsTransformation, ClaimsTransformation>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDialogService, DialogService>();
            services.AddScoped<IUserViewService, UserViewService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAdminViewService, AdminViewService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IPostViewService, PostViewService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IGroupViewService, GroupViewService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupPostUploadService, GroupPostUploadService>();
            services.AddScoped<IFriendService, FriendService>();

            return services;
        }
    }
}
