// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace PathCase.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("pathcase", "Path Case")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientId = "PathCaseClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(
                            "5oDM46~urBMPpUjaS!QQ7Y2pX7RlL8I3gh6E!vS&i#iQ$u2EiZ$f0&gn?T5wGQ*DEDYeM@Dm^7TM784#fMc4tzr5h_k#sb8z99Df"
                                .Sha256()),
                    },
                    AllowedScopes = {"pathcase"}
                }
            };
    }
}