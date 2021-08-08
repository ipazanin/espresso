// ApiKeyAuthenticationOptions.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Authentication;

namespace Espresso.WebApi.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultScheme = "API Key";

        /// <summary>
        /// 
        /// </summary>
        public string Scheme => DefaultScheme;

        /// <summary>
        /// 
        /// </summary>
        public string AuthenticationType => DefaultScheme;
    }
}
