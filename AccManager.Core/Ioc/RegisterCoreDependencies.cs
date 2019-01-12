using Microsoft.Extensions.DependencyInjection;
using AccManager.Core.Services.Interfaces;
using AccManager.Core.Services.Implementations;

namespace AccManager.Core.Ioc
{
    public static class RegisterCoreDependencies
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}
