using System.Collections.Generic;
using System.Net.Http;

namespace Espresso.Application.Extensions
{
    public static class HttpClientExtension
    {
        public static HttpClient AddHeadersToHttpClient(
            this HttpClient httpClient,
            IEnumerable<(string headerKey, string headerValue)> httpHeaders
        )
        {
            foreach (var (headerKey, headerValue) in httpHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(headerKey, headerValue);
            }

            return httpClient;
        }
    }
}
