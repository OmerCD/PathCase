using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;

namespace PathCase.IdentityServer
{
    public class RequestValidator : ICustomTokenRequestValidator
    {
        // ...
    
        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var client = context.Result.ValidatedRequest.Client;
            var userName = context.Result.ValidatedRequest.Raw.Get("userName");
            var clientClaim = new Claim(ClaimTypes.Name, userName);
            context.Result.ValidatedRequest.ClientClaims.Add(clientClaim);
            context.Result.ValidatedRequest.Client.ClientClaimsPrefix = "";
        }
    }
}