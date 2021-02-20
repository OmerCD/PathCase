using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using MediatR;
using Microsoft.Extensions.Options;
using PathCase.Core.ValueObjects;
using PathCase.Models.Authentication;

namespace PathCase.Domain.Commands
{
    public class LoginCommand : IRequest<LoginResponseModel>
    {
        public string UserName { get; init; }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseModel>
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IdentityServerOptions _identityServerOptions;

        public LoginCommandHandler(IHttpClientFactory httpFactory, IOptions<IdentityServerOptions> identityServerOptions)
        {
            _httpFactory = httpFactory;
            _identityServerOptions = identityServerOptions.Value;
        }

        public async Task<LoginResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var identityServerClient = _httpFactory.CreateClient("IdentityServer");
            var discoveryDocument = await identityServerClient.GetDiscoveryDocumentAsync(cancellationToken: cancellationToken);

            
            var tokenRequest = new ClientCredentialsTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientSecret = _identityServerOptions.Secret,
                ClientId = _identityServerOptions.ClientId,
                GrantType = OidcConstants.GrantTypes.ClientCredentials,
                Scope = _identityServerOptions.Scope,
                Parameters = new Parameters(new KeyValuePair<string, string>[]
                {
                    new("userName",request.UserName)
                })
            };
            var requestClientCredentialsToken = await identityServerClient.RequestClientCredentialsTokenAsync(tokenRequest, cancellationToken: cancellationToken);
            return new LoginResponseModel()
            {
                Token = requestClientCredentialsToken.AccessToken
            };
        }
    }
}