// SecurityHeadersBuilder.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Globalization;
using Espresso.Common.Constants;

namespace Espresso.Application.Middleware.SecurityHeaders;

/// <summary>
/// Exposes methods to build a policy.
/// </summary>
public class SecurityHeadersBuilder
{
    private static readonly CompositeFormat s_frameOptionsAllowFromUriCompositeFormat = CompositeFormat.Parse(HttpHeaderConstants.FrameOptionsAllowFromUri);

    private static readonly CompositeFormat s_xssProtectionReportCompositeFormat = CompositeFormat.Parse(HttpHeaderConstants.XssProtectionReport);

    private static readonly CompositeFormat s_strictTransportSecurityMaxAgeCompositeFormat = CompositeFormat.Parse(HttpHeaderConstants.StrictTransportSecurityMaxAge);

    private static readonly CompositeFormat s_strictTransportSecurityMaxAgeIncludeSubdomainsCompositeFormat = CompositeFormat.Parse(HttpHeaderConstants.StrictTransportSecurityMaxAgeIncludeSubdomains);

    private readonly SecurityHeadersPolicy _policy = new();

    /// <summary>
    /// Add default headers in accordance with most secure approach.
    /// </summary>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddDefaultSecurePolicy()
    {
        AddFrameOptionsDeny();
        AddXssProtectionBlock();
        AddContentTypeOptionsNoSniff();
        AddStrictTransportSecurityMaxAge();

        return this;
    }

    /// <summary>
    /// Add X-Frame-Options DENY to all requests.
    /// The page cannot be displayed in a frame, regardless of the site attempting to do so.
    /// </summary>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddFrameOptionsDeny()
    {
        _policy.SetHeaders[HttpHeaderConstants.FrameOptionsHeader] = HttpHeaderConstants.FrameOptionsDeny;
        return this;
    }

    /// <summary>
    /// Add X-Frame-Options SAMEORIGIN to all requests.
    /// The page can only be displayed in a frame on the same origin as the page itself.
    /// </summary>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddFrameOptionsSameOrigin()
    {
        _policy.SetHeaders[HttpHeaderConstants.FrameOptionsHeader] = HttpHeaderConstants.FrameOptionsSameOrigin;
        return this;
    }

    /// <summary>
    /// Add X-Frame-Options ALLOW-FROM {uri} to all requests, where the uri is provided
    /// The page can only be displayed in a frame on the specified origin.
    /// </summary>
    /// <param name="uri">The uri of the origin in which the page may be displayed in a frame.</param>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddFrameOptionsSameOrigin(string uri)
    {
        _policy.SetHeaders[HttpHeaderConstants.FrameOptionsHeader] = string.Format(CultureInfo.InvariantCulture, s_frameOptionsAllowFromUriCompositeFormat, uri);
        return this;
    }

    /// <summary>
    /// Add X-XSS-Protection 1 to all requests.
    /// Enables the XSS Protections.
    /// </summary>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddXssProtectionEnabled()
    {
        _policy.SetHeaders[HttpHeaderConstants.XssProtectionHeader] = HttpHeaderConstants.XssProtectionEnabled;
        return this;
    }

    /// <summary>
    /// Add X-XSS-Protection 0 to all requests.
    /// Disables the XSS Protections offered by the user-agent.
    /// </summary>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddXssProtectionDisabled()
    {
        _policy.SetHeaders[HttpHeaderConstants.XssProtectionHeader] = HttpHeaderConstants.XssProtectionDisabled;
        return this;
    }

    /// <summary>
    /// Add X-XSS-Protection 1; mode=block to all requests.
    /// Enables XSS protections and instructs the user-agent to block the response in the event that script has been inserted from user input, instead of sanitizing.
    /// </summary>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddXssProtectionBlock()
    {
        _policy.SetHeaders[HttpHeaderConstants.XssProtectionHeader] = HttpHeaderConstants.XssProtectionBlock;
        return this;
    }

    /// <summary>
    /// Add X-XSS-Protection 1; report=http://site.com/report to all requests.
    /// A partially supported directive that tells the user-agent to report potential XSS attacks to a single URL. Data will be POST'd to the report URL in JSON format.
    /// </summary>
    /// <param name="reportUrl">Report url.</param>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddXssProtectionReport(string reportUrl)
    {
        _policy.SetHeaders[HttpHeaderConstants.XssProtectionHeader] = string.Format(CultureInfo.InvariantCulture, s_xssProtectionReportCompositeFormat, reportUrl);
        return this;
    }

    /// <summary>
    /// Add Strict-Transport-Security max-age=<see cref="DateTimeConstants.OneYearInSeconds"/> to all requests.
    /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided.
    /// </summary>
    /// <param name="maxAge">Max age of STS header in seconds.</param>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddStrictTransportSecurityMaxAge(int maxAge = DateTimeConstants.OneYearInSeconds)
    {
        _policy.SetHeaders[HttpHeaderConstants.StrictTransportSecurityHeader] = string.Format(CultureInfo.InvariantCulture, s_strictTransportSecurityMaxAgeCompositeFormat, maxAge);
        return this;
    }

    /// <summary>
    /// Add Strict-Transport-Security max-age=<see cref="DateTimeConstants.OneYearInSeconds"/>; includeSubDomains to all requests.
    /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided and include any sub-domains.
    /// </summary>
    /// <param name="maxAge">Max age of STS header in seconds.</param>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddStrictTransportSecurityMaxAgeIncludeSubDomains(int maxAge = DateTimeConstants.OneYearInSeconds)
    {
        _policy.SetHeaders[HttpHeaderConstants.StrictTransportSecurityHeader] = string.Format(CultureInfo.InvariantCulture, s_strictTransportSecurityMaxAgeIncludeSubdomainsCompositeFormat, maxAge);
        return this;
    }

    /// <summary>
    /// Add Strict-Transport-Security max-age=0 to all requests.
    /// Tells the user-agent to remove, or not cache the host in the STS cache.
    /// </summary>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddStrictTransportSecurityNoCache()
    {
        _policy.SetHeaders[HttpHeaderConstants.StrictTransportSecurityHeader] = HttpHeaderConstants.StrictTransportSecurityNoCache;
        return this;
    }

    /// <summary>
    /// Add X-Content-Type-Options nosniff to all requests.
    /// Can be set to protect against MIME type confusion attacks.
    /// </summary>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddContentTypeOptionsNoSniff()
    {
        _policy.SetHeaders[HttpHeaderConstants.ContentTypeOptionsHeader] = HttpHeaderConstants.ContentTypeOptionsNoSniff;
        return this;
    }

    /// <summary>
    /// Adds a custom header to all requests.
    /// </summary>
    /// <param name="header">The header name.</param>
    /// <param name="value">The value for the header.</param>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder AddCustomHeader(string header, string value)
    {
        if (string.IsNullOrEmpty(header))
        {
            throw new ArgumentNullException(nameof(header));
        }

        _policy.SetHeaders[header] = value;
        return this;
    }

    /// <summary>
    /// Remove a header from all requests.
    /// </summary>
    /// <param name="header">The to remove.</param>
    /// <returns>Current <see cref="SecurityHeadersBuilder"/>.</returns>
    public SecurityHeadersBuilder RemoveHeader(string header)
    {
        if (string.IsNullOrEmpty(header))
        {
            throw new ArgumentNullException(nameof(header));
        }

        _policy.RemoveHeaders.Add(header);
        return this;
    }

    /// <summary>
    /// Builds a new <see cref="SecurityHeadersPolicy"/> using the entries added.
    /// </summary>
    /// <returns>The constructed <see cref="SecurityHeadersPolicy"/>.</returns>
    public SecurityHeadersPolicy Build()
    {
        return _policy;
    }
}
