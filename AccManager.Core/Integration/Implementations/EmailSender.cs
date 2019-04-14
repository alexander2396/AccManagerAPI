using System;
using System.Linq;
using System.Collections.Generic;
using MimeKit;
using Microsoft.AspNetCore.Http;
using AccManager.Common.RequestResult;
using AccManager.Models.ViewModels.Mail;
using AccManager.Common.Enums;
using AccManager.Models.ViewModels;
using AccManager.Common.ExtensionMethods;

namespace AccManager.Core.Integration.Implementations
{
    public abstract class EmailSender
    {
        protected RequestResult<MimeMessage> BuildMimeMessage(MessageModel message)
        {
            RequestResult<MimeMessage> result = new RequestResult<MimeMessage>();

            if (!message.From.IsValidEmail())
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Message = "Invalid from email";
                return result;
            }

            string[] toEmailAddresses = message.To.Split(new[] { ';', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (!toEmailAddresses.Any() || toEmailAddresses.Any(email => !email.IsValidEmail()))
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Message = "Invalid to email";
                return result;
            }

            MimeMessage mimeMessage = new MimeMessage();

            var builder = new BodyBuilder();

            mimeMessage.From.Add(new MailboxAddress(message.FromName, message.From));
            mimeMessage.Sender = new MailboxAddress(message.FromName, message.From);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = BuildBody(message);
            mimeMessage.To.AddRange(toEmailAddresses.Select(email => new MailboxAddress(email)));

            result.Obj = mimeMessage;
            result.StatusCode = StatusCodes.Status200OK;

            return result;
        }

        private MimeEntity BuildBody(MessageModel message)
        {
            BodyBuilder resultBuilder = new BodyBuilder();

            if (message.BodyType == EEmailBodyType.Html)
            {
                resultBuilder.HtmlBody = message.Body;
            }
            else
            {
                resultBuilder.TextBody = message.Body;
            }

            foreach (var attach in message.Attachments ?? new List<FileViewModel>())
            {
                resultBuilder.Attachments.Add(attach.Name, attach.Body);
            }

            return resultBuilder.ToMessageBody();
        }
    }
}
