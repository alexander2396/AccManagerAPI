using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using AccManager.Common.Enums;
using AccManager.Common.Constants;

namespace AccManager.WebApi.Attributes
{
    public class AuthorizePermissionsAttribute : TypeFilterAttribute
    {
        public AuthorizePermissionsAttribute(params Permission[] permissions) : base(typeof(AuthorizePermissionsFilter))
        {
            var claims = new List<Claim>();
            foreach (var permission in permissions)
            {
                claims.Add(new Claim(AppConstants.Claims.PERMISSION_CLAIM_TYPE, permission.ToString()));
            }

            Arguments = new object[] { claims };
        }
    }

    public class AuthorizePermissionsFilter : IAuthorizationFilter
    {
        readonly List<Claim> _claims;

        public AuthorizePermissionsFilter(List<Claim> claims)
        {
            _claims = claims;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (!context.HttpContext.User.HasClaim(AppConstants.Claims.PERMISSION_CLAIM_TYPE, Permission.Admin.ToString()))
            {
                bool hasClaims = true;
                foreach (var claim in _claims)
                {
                    bool hasClaim = false;
                    foreach (var uClaim in context.HttpContext.User.Claims)
                    {
                        if (uClaim.Type == claim.Type && uClaim.Value == claim.Value)
                        {
                            hasClaim = true;
                            break;
                        }
                    }

                    if (!hasClaim)
                    {
                        hasClaims = false;
                    }
                }

                if (!hasClaims)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
