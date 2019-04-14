using System.Collections.Generic;
using System.Threading.Tasks;
using AccManager.Common.RequestResult;
using AccManager.Models.ViewModels.Account;

namespace AccManager.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<RequestResult<List<UserViewModel>>> GetListAsync();
        Task<RequestResult<UserViewModel>> GetByIdAsync(int id);
        Task<RequestResult<UserViewModel>> CreateAsync(UserViewModel userViewModel);
        Task<RequestResult<UserViewModel>> UpdateAsync(UserViewModel userViewModel);
        Task<RequestResult> DeleteAsync(int id);
    }
}
