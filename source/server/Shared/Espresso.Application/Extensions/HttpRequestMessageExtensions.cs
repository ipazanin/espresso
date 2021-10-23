// HttpRequestMessageExtensions.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Net.Http;

namespace Espresso.Application.Extensions
{
    /// <summary>
    /// Http request message extensions.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Tries to mimic web browser request by adding HTTP headers to <paramref name="request"/>.
        /// </summary>
        /// <param name="request">HTTP request message.</param>
        /// <returns>The <paramref name="request"/>.</returns>
        public static HttpRequestMessage AddBrowserHeadersToHttpRequestMessage(
            this HttpRequestMessage request)
        {
            request.Headers.TryAddWithoutValidation("accept", "*/*");
            request.Headers.TryAddWithoutValidation("accept-encoding", "gzip, deflate");
            request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Mobile Safari/537.36");

            return request;
        }
    }
}
