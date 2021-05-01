using System.Net.Http;

namespace Espresso.Application.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddBrowserHeadersToHttpRequestMessage(
            this HttpRequestMessage request
        )
        {
            request.Headers.TryAddWithoutValidation("accept", "*/*");
            request.Headers.TryAddWithoutValidation("accept-encoding", "gzip, deflate");
            request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Mobile Safari/537.36");

            return request;
        }
    }
}
