// ApiKeyAuthenticationOptions.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Authentication;

namespace Espresso.WebApi.Authentication;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "API Key";

    public string Scheme => DefaultScheme;

    public string AuthenticationType => DefaultScheme;
}
