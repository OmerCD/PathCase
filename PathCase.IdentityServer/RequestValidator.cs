using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;

namespace PathCase.IdentityServer
{
    public class RequestValidator : ICustomTokenRequestValidator
    {
        private readonly ILogger<RequestValidator> _logger;

        public RequestValidator(ILogger<RequestValidator> logger)
        {
            _logger = logger;
        }

        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            try
            {
                var userName = context.Result.ValidatedRequest.Raw.Get("userName");
                var clientClaim = new Claim(ClaimTypes.Name, userName);
                context.Result.ValidatedRequest.ClientClaims.Add(clientClaim);
                context.Result.ValidatedRequest.Client.ClientClaimsPrefix = "";
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Critical", ex);
            }

            return Task.CompletedTask;
        }
    }
}