using System.Net.Http;

namespace Espresso.Application.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddBrowserHeadersToHttpRequestMessage(
            this HttpRequestMessage request
        )
        {
            // _ = request.Headers.TryAddWithoutValidation("accept", "text/html,application/xhtml+xml,application/xml");
            // _ = request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            // _ = request.Headers.TryAddWithoutValidation("accept-charset", "ISO-8859-1");

            request.Headers.TryAddWithoutValidation("accept", "*/*");
            request.Headers.TryAddWithoutValidation("accept-encoding", "gzip, deflate");
            request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Mobile Safari/537.36");

            return request;
        }
    }
}
