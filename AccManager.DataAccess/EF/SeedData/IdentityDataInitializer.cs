using System;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using AccManager.Models.BusinessModels.Account;
using AccManager.Common.Constants;
using AccManager.Common.Enums;

namespace AccManager.DataAccess.EF.SeedData
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedData(IServiceProvider services)
        {
            UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();

            User user = await userManager.FindByNameAsync(AppConstants.ADMIN_USER_NAME);
            if (user == null)
            {
                user = new User()
                {
                    UserName = AppConstants.ADMIN_USER_NAME,
                    Email = AppConstants.ADMIN_USER_EMAIL
                };

                IdentityResult result = await userManager.CreateAsync(user, AppConstants.ADMIN_USER_PASSWORD);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot create user: " + result.Errors.FirstOrDefault());
                }

                await userManager.AddClaimAsync(user, new Claim(AppConstants.Claims.PERMISSION_CLAIM_TYPE, Permission.Admin.ToString()));
            }
        }
    }
}
