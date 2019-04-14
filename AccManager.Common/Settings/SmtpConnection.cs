using Microsoft.Extensions.Configuration;

namespace AccManager.Common.Settings
{
    public class SmtpConnection
    {
        private readonly IConfigurationSection _smtpSection;

        public SmtpConnection(IConfigurationSection smtpSection)
        {
            _smtpSection = smtpSection;
        }

        public string SERVER => _smtpSection["SERVER"] ?? string.Empty;
        public int PORT => int.Parse(_smtpSection["PORT"]);
        public string LOGIN => _smtpSection["LOGIN"] ?? string.Empty;
        public string PASSWORD => _smtpSection["PASSWORD"] ?? string.Empty;
        public string FROM => _smtpSection["FROM"] ?? string.Empty;
    }
}
