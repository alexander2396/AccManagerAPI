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
            SmtpConnection = new SmtpConnection(configurationRoot.GetSection("SMTP_CONNECTION"));
        }

        public ConnectionStrings ConnectionStrings { get; }
        public AuthOptions AuthOptions { get; }
        public SmtpConnection SmtpConnection { get; }
    }
}
