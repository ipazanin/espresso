using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.Application.IServices
{
    public interface IHttpService
    {
        public Task PostJsonAsync<TData>(
            string url,
            TData data,
            IEnumerable<(string headerName, string headerValue)>? httpHeaders,
            TimeSpan httpClientTimeout,
            CancellationToken cancellationToken
        );
    }
}
