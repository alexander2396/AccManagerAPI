using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AccManager.Models.BusinessModels.Account;
using AccManager.Common.RequestResult;
using AccManager.Core.Services.Interfaces;
using AccManager.Models.ViewModels.Account;
using AccManager.DataAccess.UOW;

namespace AccManager.Core.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _uow;

        public RoleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<RequestResult<List<RoleViewModel>>> GetListAsync()
        {
            var result = new RequestResult<List<RoleViewModel>>();

            try
            {
                List<Role> roles = await _uow.Repository<Role>()
                    .GetQueryable()
                    .Include(r => r.Permissions)
                    .ToListAsync();

                result.Obj = roles.Select(r => new RoleViewModel().SetFrom(r)).ToList();
                result.StatusCode = StatusCodes.Status200OK;
            }
            catch
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<RequestResult<RoleViewModel>> GetByIdAsync(int id)
        {
            var result = new RequestResult<RoleViewModel>();

            try
            {
                Role role = await _uow.Repository<Role>()
                    .GetQueryable()
                    .Include(r => r.Permissions)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (role == null)
                {
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    result.Message = $"Role with Id = {id} not found";
                    return result;
                }

                result.Obj = new RoleViewModel().SetFrom(role);
                result.StatusCode = StatusCodes.Status200OK;
            }
            catch
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<RequestResult<RoleViewModel>> CreateAsync(RoleViewModel roleViewModel)
        {
            var result = new RequestResult<RoleViewModel>();

            try
            {
                Role role = roleViewModel.UpdateEntity(new Role());

                _uow.Repository<Role>().Save(role);
                await _uow.SaveAsync();

                result.Obj = new RoleViewModel().SetFrom(role);
                result.StatusCode = StatusCodes.Status200OK;
            }
            catch
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<RequestResult<RoleViewModel>> UpdateAsync(RoleViewModel roleViewModel)
        {
            var result = new RequestResult<RoleViewModel>();

            try
            {
                Role role = await _uow.Repository<Role>()
                    .GetQueryable()
                    .Include(r => r.Permissions)
                    .FirstOrDefaultAsync(r => r.Id == roleViewModel.Id);

                if (role == null)
                {
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    return result;
                }

                _uow.Repository<Role>().Update(roleViewModel.UpdateEntity(role));
                await _uow.SaveAsync();

                result.Obj = new RoleViewModel().SetFrom(role);
                result.StatusCode = StatusCodes.Status200OK;
            }
            catch
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<RequestResult> DeleteAsync(int id)
        {
            var result = new RequestResult();

            try
            {
                Role role = await _uow.Repository<Role>().GetByIdAsync(id);

                if (role == null)
                {
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    return result;
                }

                _uow.Repository<Role>().Delete(role);
                await _uow.SaveAsync();
                
                result.StatusCode = StatusCodes.Status200OK;
            }
            catch
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return result;
        }
    }
}
