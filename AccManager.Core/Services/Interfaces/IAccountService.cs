using System.Threading.Tasks;
using AccManager.Common.RequestResult;
using AccManager.Models.ViewModels.Account;

namespace AccManager.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RequestResult<TokenResult>> GetTokenAsync(string email, string password);
    }
}
