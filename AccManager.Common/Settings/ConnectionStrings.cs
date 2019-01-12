using Microsoft.Extensions.Configuration;

namespace AccManager.Common.Settings
{
    public class ConnectionStrings
    {
        private readonly IConfigurationSection _connectionStringsSection;

        public ConnectionStrings(IConfigurationSection connectionStringsSection)
        {
            _connectionStringsSection = connectionStringsSection;
        }

        public string ACC_MANAGER_APPLICATION => _connectionStringsSection["ACC_MANAGER_APPLICATION"];
        public string ACC_MANAGER_IDENTITY => _connectionStringsSection["ACC_MANAGER_IDENTITY"];
    }
}
