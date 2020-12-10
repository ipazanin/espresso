using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.Application.Utilities
{
    public class RetryHttpRequestHandler : DelegatingHandler
    {
        private readonly int _maxRetries;

        public RetryHttpRequestHandler(
            int maxRetries
        ) : base()
        {
            if (maxRetries < 1)
            {
                throw new ArgumentException(
                    message: $"{nameof(maxRetries)} must be greater than 0!",
                    paramName: nameof(maxRetries)
                );
            }
            _maxRetries = maxRetries;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            HttpResponseMessage? response = null;
            for (var i = 0; i < _maxRetries; i++)
            {
                response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
            }

            if (response is null)
            {
                throw new Exception($"Http Request Failed With {_maxRetries} retries!");
            }

            return response;
        }
    }
}
