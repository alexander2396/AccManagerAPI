using System;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AccManager.Common.RequestResult;
using AccManager.Common.Settings;
using AccManager.Models.ViewModels.Mail;
using AccManager.Core.Integration.Interfaces;

namespace AccManager.Core.Integration.Implementations
{
    public class SmtpEmailSender : EmailSender, ISenderProvider
    {
        private readonly IAppSettings _appSettings;
        private readonly ILogger _logger;

        public SmtpEmailSender(IAppSettings appSettings, ILoggerFactory loggerFactory)
        {
            _appSettings = appSettings;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public async Task<RequestResult> SendMessageAsync(MessageModel message)
        {
            RequestResult result = new RequestResult();

            RequestResult<MimeMessage> messageResult = BuildMimeMessage(message);

            if (messageResult.StatusCode != StatusCodes.Status200OK)
            {
                result.StatusCode = messageResult.StatusCode;
                result.Message = messageResult.Message;
                return result;
            }

            using (SmtpClient smtpClient = new SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_appSettings.SmtpConnection.SERVER, _appSettings.SmtpConnection.PORT, false);

                    await smtpClient.AuthenticateAsync(_appSettings.SmtpConnection.LOGIN, _appSettings.SmtpConnection.PASSWORD);

                    await smtpClient.SendAsync(messageResult.Obj);
                    await smtpClient.DisconnectAsync(true);

                    result.StatusCode = StatusCodes.Status200OK;
                }
                catch (Exception ex)
                {
                    result.SetStatusInternalServerError();
                }
            }

            return result;
        }
    }
}
