using System.Threading.Tasks;
using AccManager.Common.RequestResult;

namespace AccManager.Core.Services.Interfaces
{
    public interface IMessagingService
    {
        Task<RequestResult> SendInvitationEmailAsync(string email, string code);
    }
}
