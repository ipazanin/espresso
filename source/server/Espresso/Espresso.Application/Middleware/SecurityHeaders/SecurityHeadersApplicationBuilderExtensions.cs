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
        /// <param name="app"></param>
        /// <param name="securityHeadersBuilderConfigurationAction"></param>
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
