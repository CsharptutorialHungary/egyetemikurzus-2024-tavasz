using CommunitySite.Data.Context;
using CommunitySite.Services.AdminServices;
using CommunitySite.Services.AdminViewServices;
using CommunitySite.Services.ClaimsServices;
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
        public static IServiceCollection RegisterDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConns = configuration.GetConnectionString("default");
            services.AddPooledDbContextFactory<CommunitySiteContext>(options => options
                .UseOracle(defaultConns)
                .EnableSensitiveDataLogging()
            );

            return services;
        }

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

            return services;
        }
    }
}
