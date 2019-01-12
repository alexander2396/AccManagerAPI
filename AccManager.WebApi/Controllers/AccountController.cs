using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using AccManager.Core.Services.Interfaces;
using AccManager.Common.RequestResult;
using AccManager.Models.ViewModels.Account;

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
        public async Task<IActionResult> Token([FromBody]LoginViewModel model)
        {
            RequestResult<TokenResult> result = await _accountService.GetToken(model.Email, model.Password);

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
