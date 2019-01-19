using System;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AccManager.Models.BusinessModels.Account;
using AccManager.Common.Settings;
using AccManager.Common.RequestResult;
using AccManager.Core.Services.Interfaces;
using AccManager.Models.ViewModels.Account;

namespace AccManager.Core.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        private readonly IAppSettings _appSettings;

        public AccountService(UserManager<User> userManager, SignInManager<User> singInManager, IAppSettings appSettings)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _appSettings = appSettings;
        }

        public async Task<RequestResult<TokenResult>> GetTokenAsync(string email, string password)
        {
            var result = new RequestResult<TokenResult>();

            try
            {
                ClaimsIdentity identity = await GetIdentity(email, password);

                if (identity == null)
                {
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    result.Message = "Wrong username or password";
                    return result;
                }

                DateTime now = DateTime.UtcNow;

                JwtSecurityToken jwt = new JwtSecurityToken(
                        issuer: _appSettings.AuthOptions.ISSUER,
                        audience: _appSettings.AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(double.Parse(_appSettings.AuthOptions.LIFETIME))),
                        signingCredentials:
                            new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.AuthOptions.KEY)),
                            SecurityAlgorithms.HmacSha256));
                string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                result.StatusCode = StatusCodes.Status200OK;
                result.Obj = new TokenResult()
                {
                    Token = encodedJwt,
                    Email = email
                };
            }
            catch
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user != null && (await _singInManager.PasswordSignInAsync(user, password, false, false)).Succeeded)
            {
                var claims = new[]
                {
                   new Claim(JwtRegisteredClaimNames.Sub, user.Id)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
