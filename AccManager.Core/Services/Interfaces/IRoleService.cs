using System.Collections.Generic;
using System.Threading.Tasks;
using AccManager.Common.RequestResult;
using AccManager.Models.ViewModels.Account;

namespace AccManager.Core.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RequestResult<List<RoleViewModel>>> GetListAsync();
        Task<RequestResult<RoleViewModel>> GetByIdAsync(int id);
        Task<RequestResult<RoleViewModel>> CreateAsync(RoleViewModel roleViewModel);
        Task<RequestResult<RoleViewModel>> UpdateAsync(RoleViewModel roleViewModel);
        Task<RequestResult> DeleteAsync(int id);
    }
}
