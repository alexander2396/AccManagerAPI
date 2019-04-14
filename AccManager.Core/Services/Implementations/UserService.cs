using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AccManager.Models.BusinessModels.Account;
using AccManager.Common.RequestResult;
using AccManager.Core.Services.Interfaces;
using AccManager.Models.ViewModels.Account;
using AccManager.DataAccess.UOW;

namespace AccManager.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMessagingService _messagingService;

        public UserService(IUnitOfWork uow, UserManager<IdentityUser> userManager, IMessagingService messagingService)
        {
            _uow = uow;
            _userManager = userManager;
            _messagingService = messagingService;
        }

        public async Task<RequestResult<List<UserViewModel>>> GetListAsync()
        {
            var result = new RequestResult<List<UserViewModel>>();

            try
            {
                List<User> users = await _uow.Repository<User>()
                    .GetQueryable()
                    .Include(u => u.Role)
                    .ToListAsync();

                result.Obj = users.Select(r => new UserViewModel().SetFrom(r)).ToList();
                result.StatusCode = StatusCodes.Status200OK;
            }
            catch
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<RequestResult<UserViewModel>> GetByIdAsync(int id)
        {
            var result = new RequestResult<UserViewModel>();

            try
            {
                User user = await _uow.Repository<User>()
                    .GetQueryable()
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    result.Message = $"User with Id = {id} not found";
                    return result;
                }

                result.Obj = new UserViewModel().SetFrom(user);
                result.StatusCode = StatusCodes.Status200OK;
            }
            catch
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<RequestResult<UserViewModel>> CreateAsync(UserViewModel userViewModel)
        {
            var result = new RequestResult<UserViewModel>();

            try
            {
                User user = userViewModel.UpdateEntity(new User());

                var identityUser = new IdentityUser()
                {
                    Email = userViewModel.Email
                };

                IdentityResult identityResult = await _userManager.CreateAsync(identityUser);
                if (!identityResult.Succeeded)
                {
                    throw new Exception("Cannot create user: " + identityResult.Errors.FirstOrDefault());
                }

                user.IdentityUserId = identityUser.Id;

                string passwordCreateCode = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                var sendEmailResult = await _messagingService.SendInvitationEmailAsync(user.Email, passwordCreateCode);
                if (!sendEmailResult.IsSuccess)
                {
                    result.SetStatusInternalServerError("Error while creating password create token");
                }

                _uow.Repository<User>().Save(user);
                await _uow.SaveAsync();

                result.Obj = new UserViewModel().SetFrom(user);
                result.SetStatusOK();
            }
            catch
            {
                result.SetStatusInternalServerError();
            }

            return result;
        }

        public async Task<RequestResult<UserViewModel>> UpdateAsync(UserViewModel userViewModel)
        {
            var result = new RequestResult<UserViewModel>();

            try
            {
                User user = await _uow.Repository<User>()
                   .GetQueryable()
                   .Include(u => u.Role)
                   .FirstOrDefaultAsync(u => u.Id == userViewModel.Id);

                if (user == null)
                {
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    result.Message = $"User with Id = {userViewModel.Id} not found";
                    return result;
                }

                _uow.Repository<User>().Update(userViewModel.UpdateEntity(user));
                await _uow.SaveAsync();

                result.Obj = new UserViewModel().SetFrom(user);
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
                User user = await _uow.Repository<User>().GetByIdAsync(id);

                if (user == null)
                {
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    result.Message = $"User with Id = {id} not found";
                    return result;
                }

                _uow.Repository<User>().Delete(user);
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
