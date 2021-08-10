// SecurityHeadersApplicationBuilderExtensions.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using Microsoft.AspNetCore.Builder;

namespace Espresso.Application.Middleware.SecurityHeaders
{
    /// <summary>
    /// Security Headers Extensions.
    /// </summary>
    public static class SecurityHeadersApplicationBuilderExtensions
    {
        /// <summary>
        /// Sets Security Headers.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="securityHeadersBuilderConfigurationAction">Security headers builder configuration.</param>
        /// <returns>A reference to this instance after operation is complete.</returns>
        public static IApplicationBuilder UseSecurityHeadersMiddleware(
            this IApplicationBuilder app,
            Action<SecurityHeadersBuilder> securityHeadersBuilderConfigurationAction
        )
        {
            var builder = new SecurityHeadersBuilder();

            securityHeadersBuilderConfigurationAction.Invoke(builder);

            var policy = builder.Build();

            return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
        }
    }
}
