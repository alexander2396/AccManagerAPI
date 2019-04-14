using System.Collections.Generic;
using AccManager.Common.Enums;

namespace AccManager.Models.ViewModels.Mail
{
    public class MessageModel
    {
        public string From { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }
        public EEmailBodyType BodyType { get; set; }
        public List<FileViewModel> Attachments { get; set; }
    }
}
