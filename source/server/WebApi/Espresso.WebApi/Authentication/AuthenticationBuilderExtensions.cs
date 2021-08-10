// AuthenticationBuilderExtensions.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using Microsoft.AspNetCore.Authentication;

namespace Espresso.WebApi.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthenticationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationBuilder"></param>
        /// <param name="options"></param>
        public static AuthenticationBuilder AddApiKeySupport(
            this AuthenticationBuilder authenticationBuilder,
            Action<ApiKeyAuthenticationOptions> options
        )
        {
            return authenticationBuilder
                .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
                    authenticationScheme: ApiKeyAuthenticationOptions.DefaultScheme,
                    configureOptions: options
                );
        }
    }
}
