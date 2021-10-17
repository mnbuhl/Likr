﻿using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace Likr.Identity.Settings
{
    public class IdentityServerSettings
    {
        private readonly string _serviceUrl;
        
        private readonly IWebHostEnvironment _env;

        public IdentityServerSettings(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _serviceUrl = configuration.GetValue<string>("ServiceUrl");
        }

        public readonly IReadOnlyCollection<ApiScope> ApiScopes = new ApiScope[]
        {
            new ApiScope { Name = "posts"  },
            new ApiScope { Name = "comments" },
            new ApiScope { Name = "likes" },
            new ApiScope { Name = "profiles" },
            new ApiScope { Name = "IdentityServerApi" },
        };

        public readonly IReadOnlyCollection<ApiResource> ApiResources = new ApiResource[]
        {
            new ApiResource
            {
                Name = "Posts",
                Scopes = new List<string> { "posts" }
            },
            new ApiResource
            {
                Name = "Comments",
                Scopes = new List<string> { "comments" }
            },
            new ApiResource
            {
                Name = "Likes",
                Scopes = new List<string> { "likes" }
            },
            new ApiResource
            {
                Name = "Profiles",
                Scopes = new List<string> { "profiles" }
            },
        };

        public IReadOnlyCollection<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId = "client-spa",
                AllowedGrantTypes = new List<string> { "implicit" },
                RequireClientSecret = false,
                RedirectUris = new List<string> {
                    "http://localhost:5004/authentication/login-callback",
                },
                AllowedScopes = new List<string>
                {
                    "openid",
                    "profile",
                    "posts",
                    "comments",
                    "likes",
                    "profiles",
                    "IdentityServerApi"
                },
                AlwaysIncludeUserClaimsInIdToken = true,
                PostLogoutRedirectUris = new List<string> { "http://localhost:5004" },
                AllowedCorsOrigins = new List<string>
                {
                    "http://localhost:5004"
                }
            },
            new Client
            {
                ClientId = "client-spa-container",
                AllowedGrantTypes = new List<string> { "implicit" },
                RequireClientSecret = false,
                RedirectUris = new List<string> {
                    "http://localhost:8100/authentication/login-callback",
                },
                AllowedScopes = new List<string>
                {
                    "openid",
                    "profile",
                    "posts",
                    "comments",
                    "likes",
                    "profiles",
                    "IdentityServerApi"
                },
                AlwaysIncludeUserClaimsInIdToken = true,
                PostLogoutRedirectUris = new List<string> { "http://localhost:8100" },
                AllowedCorsOrigins = new List<string>
                {
                    "http://localhost:8100"
                }
            },
            _env.IsDevelopment()
                ? new Client
                {
                    ClientId = "postman",
                    AllowedGrantTypes = new List<string> { "authorization_code" },
                    RequireClientSecret = false,
                    RedirectUris = new List<string> { "urn:ietf:wg:oauth:2.0:oob" },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "posts",
                        "comments",
                        "likes",
                        "profiles",
                        "IdentityServerApi"
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                }
                : new Client()
        };

        public IReadOnlyCollection<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
    }
}