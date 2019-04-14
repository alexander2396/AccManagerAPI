using System;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Http;
using AccManager.Common.RequestResult;
using AccManager.Core.Services.Interfaces;
using AccManager.Core.Integration.Interfaces;
using AccManager.Common.Constants;
using AccManager.Common.Settings;
using AccManager.Models.ViewModels.Mail;

namespace AccManager.Core.Services.Implementations
{
    public class MessagingService : IMessagingService
    {
        private readonly IAppSettings _appSettings;
        private readonly ISenderProvider _senderProvider;

        public MessagingService(IAppSettings appSettings, ISenderProvider senderProvider)
        {
            _appSettings = appSettings;
            _senderProvider = senderProvider;
        }

        public async Task<RequestResult> SendInvitationEmailAsync(string email, string code)
        {
            RequestResult result = new RequestResult();

            try
            {
                var callbackUrl = $"{AppConstants.UI_HOST}#/authorization/create-password?email={email}&code={code}";
                MessageModel messageModel = new MessageModel()
                {
                    Body = $"To create your account go to link {callbackUrl}",
                    From = _appSettings.SmtpConnection.FROM,
                    To = email,
                    Subject = "Account Manager registration"
                };

                return await _senderProvider.SendMessageAsync(messageModel);
            }
            catch
            {
                result.SetStatusInternalServerError();
            }

            return result;
        }
    }
}
