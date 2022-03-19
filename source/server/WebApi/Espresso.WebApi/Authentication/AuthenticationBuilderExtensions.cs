// AuthenticationBuilderExtensions.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Authentication;

namespace Espresso.WebApi.Authentication;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddApiKeySupport(
        this AuthenticationBuilder authenticationBuilder,
        Action<ApiKeyAuthenticationOptions> options)
    {
        return authenticationBuilder
            .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
                authenticationScheme: ApiKeyAuthenticationOptions.DefaultScheme,
                configureOptions: options);
    }
}
