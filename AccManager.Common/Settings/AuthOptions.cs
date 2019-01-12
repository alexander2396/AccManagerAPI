using Microsoft.Extensions.Configuration;

namespace AccManager.Common.Settings
{
    public class AuthOptions
    {
        private readonly IConfigurationSection _authSection;

        public AuthOptions(IConfigurationSection authSection)
        {
            _authSection = authSection;
        }

        public string ISSUER => _authSection["ISSUER"];
        public string AUDIENCE => _authSection["AUDIENCE"];
        public string KEY => _authSection["KEY"];
        public string LIFETIME => _authSection["LIFETIME"];
    }
}
