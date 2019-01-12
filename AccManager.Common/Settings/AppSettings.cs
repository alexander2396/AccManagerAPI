using Microsoft.Extensions.Configuration;

namespace AccManager.Common.Settings
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfigurationRoot _configurationRoot;

        public AppSettings(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
            ConnectionStrings = new ConnectionStrings(configurationRoot.GetSection("CONNECTION_STRINGS"));
            AuthOptions = new AuthOptions(configurationRoot.GetSection("AUTH_OPTIONS"));
        }

        public ConnectionStrings ConnectionStrings { get; }
        public AuthOptions AuthOptions { get; }
    }
}
