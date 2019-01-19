using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AccManager.Core.Services.Interfaces;
using AccManager.Common.RequestResult;
using AccManager.Models.ViewModels.Account;
using AccManager.WebApi.Filters;

namespace AccManager.WebApi.Controllers
{
    [Authorize]
    [Route("api/role")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            RequestResult<List<RoleViewModel>> result = await _roleService.GetListAsync();
            return ReturnResult(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            RequestResult<RoleViewModel> result = await _roleService.GetByIdAsync(id);
            return ReturnResult(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAsync([FromBody]RoleViewModel model)
        {
            RequestResult<RoleViewModel> result = await _roleService.CreateAsync(model);
            return ReturnResult(result);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAsync([FromBody]RoleViewModel model)
        {
            RequestResult<RoleViewModel> result = await _roleService.UpdateAsync(model);
            return ReturnResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            RequestResult result = await _roleService.DeleteAsync(id);
            return ReturnResult(result);
        }
    }
}
