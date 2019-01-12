using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AccManager.DataAccess.EF.Context;
using AccManager.Common.Settings;
using AccManager.Models.BusinessModels.Account;

namespace AccManager.DataAccess.Ioc
{
    public static class RegisterDataAccessDependencies
    {
        public static void RegisterDataAccess(this IServiceCollection services, IAppSettings appSettings)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(appSettings.ConnectionStrings.ACC_MANAGER_APPLICATION, b => {
                    b.MigrationsAssembly("AccManager.DataAccess");
                    b.MigrationsHistoryTable("__EfMigrationsHistory");
                }));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(appSettings.ConnectionStrings.ACC_MANAGER_IDENTITY, b => {
                    b.MigrationsAssembly("AccManager.DataAccess");
                }));
            services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<IdentityDbContext>();
        }
    }
}
