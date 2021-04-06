using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.UnitTests.APITests
{
    public class DisableAuthenticationPolicyEvaluator : IPolicyEvaluator
    {
        public async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            // Always pass authentication.
            var authenticationTicket = new AuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties(), JwtBearerDefaults.AuthenticationScheme);
            return await Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }

        public async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object resource)
        {
            // Always pass authorization
            return await Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}
