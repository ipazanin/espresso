// SecurityHeadersMiddleware.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Http;

namespace Espresso.Application.Middleware.SecurityHeaders;

/// <summary>
/// Security Headers Middleware.
/// </summary>
public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SecurityHeadersPolicy _policy;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
    /// Security Headers Middleware Constructor.
    /// </summary>
    /// <param name="next">Next request delegate.</param>
    /// <param name="policy">Security headers policy.</param>
    public SecurityHeadersMiddleware(RequestDelegate next, SecurityHeadersPolicy policy)
    {
        _next = next;
        _policy = policy;
    }

    /// <summary>
    /// Invokes Middleware.
    /// </summary>
    /// <param name="context">Http context.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Invoke(HttpContext context)
    {
        var headers = context.Response.Headers;

        foreach (var headerValuePair in _policy.SetHeaders)
        {
            headers[headerValuePair.Key] = headerValuePair.Value;
        }

        foreach (var header in _policy.RemoveHeaders)
        {
            headers.Remove(header);
        }

        return _next(context);
    }
}
