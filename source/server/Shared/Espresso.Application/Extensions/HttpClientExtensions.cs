// HttpClientExtensions.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Application.Extensions;

/// <summary>
/// Http client extensions.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Adds <paramref name="httpHeaders"/> to <paramref name="httpClient"/>.
    /// </summary>
    /// <param name="httpClient">HTTP client.</param>
    /// <param name="httpHeaders">HTTP headers.</param>
    /// <returns>The <paramref name="httpClient"/>.</returns>
    public static HttpClient AddHeadersToHttpClient(
        this HttpClient httpClient,
        IEnumerable<(string headerKey, string headerValue)> httpHeaders)
    {
        foreach (var (headerKey, headerValue) in httpHeaders)
        {
            httpClient.DefaultRequestHeaders.Add(headerKey, headerValue);
        }

        return httpClient;
    }
}
