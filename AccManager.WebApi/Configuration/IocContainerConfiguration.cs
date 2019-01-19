using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AccManager.Common.Settings;
using AccManager.DataAccess.Ioc;
using AccManager.Core.Ioc;
using AccManager.WebApi.Filters;

namespace AccManager.WebApi.Configuration
{
    public static class IocContainerConfiguration
    {
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            IAppSettings appSettings = services.BuildServiceProvider().GetService<IAppSettings>();

            services.RegisterDataAccess(appSettings);
            services.RegisterCore();

            services.AddScoped<ValidationFilterAttribute>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = appSettings.AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.AuthOptions.KEY)),
                    ValidateIssuerSigningKey = true,
                };
            });
        }
    }
}
