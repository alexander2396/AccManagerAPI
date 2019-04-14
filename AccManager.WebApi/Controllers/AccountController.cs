using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AccManager.Core.Services.Interfaces;
using AccManager.Common.RequestResult;
using AccManager.Models.ViewModels.Account;
using AccManager.WebApi.Attributes;

namespace AccManager.WebApi.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("token")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> TokenAsync([FromBody]LoginViewModel model)
        {
            RequestResult<TokenResult> result = await _accountService.GetTokenAsync(model.Email, model.Password);
            return ReturnResult(result);
        }

        [Authorize]
        [Route("test")]
        public string Test()
        {
            return "OK";
        }
    }
}
