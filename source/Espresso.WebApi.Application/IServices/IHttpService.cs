using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Application.IServices
{
    public interface IHttpService
    {
        public Task<HttpResponseMessage> PostJsonAsync<TData>(
            string url,
            TData data,
            IEnumerable<(string headerName, string headerValue)>? httpHeaders,
            TimeSpan httpClientTimeout,
            CancellationToken cancellationToken
        );
    }
}
