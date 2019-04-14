using System.Threading.Tasks;
using AccManager.Common.RequestResult;
using AccManager.Models.ViewModels.Mail;

namespace AccManager.Core.Integration.Interfaces
{
    public interface ISenderProvider
    {
        Task<RequestResult> SendMessageAsync(MessageModel message);
    }
}
