﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Likr.Likes.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddIdentityServerAuth(this IServiceCollection services,
            IConfiguration configuration)
        {
            string identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                });
            
            return services;
        }
    }
}