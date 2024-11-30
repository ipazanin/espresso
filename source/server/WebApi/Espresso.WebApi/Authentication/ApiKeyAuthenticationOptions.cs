// ApiKeyAuthenticationOptions.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Authentication;

namespace Espresso.WebApi.Authentication;

public sealed class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "API Key";

    public static string Scheme => DefaultScheme;

    public static string AuthenticationType => DefaultScheme;
}
