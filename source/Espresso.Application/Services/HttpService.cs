using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Extensions;
using Espresso.Common.Constants;
using Espresso.Common.Utilities;
using Espresso.Wepi.Application.IServices;

namespace Espresso.Application.Services
{
    public class HttpService : IHttpService
    {
        #region Fields
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion


        #region Constructors
        public HttpService(
            IHttpClientFactory httpClientFactory
        )
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Methods
        public async Task<HttpResponseMessage> PostJsonAsync<TData>(
            string url,
            TData data,
            IEnumerable<(string headerName, string headerValue)>? httpHeaders,
            TimeSpan httpClientTimeout,
            CancellationToken cancellationToken
        )
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = httpClientTimeout;
            httpClient.AddHeadersToHttpClient(httpHeaders);

            var httpContent = await CreateJsonHttpContent(
                data: data,
                cancellationToken: cancellationToken
            );
            var response = await httpClient
                .PostAsync(
                    requestUri: url,
                    content: httpContent,
                    cancellationToken: cancellationToken
                );

            response.EnsureSuccessStatusCode();

            return response;
        }

        private static async Task<HttpContent> CreateJsonHttpContent<TData>(
            TData data,
            CancellationToken cancellationToken
        )
        {
            var jsonString = await JsonUtility.Serialize(data, cancellationToken);

            var content = new StringContent(
                content: jsonString,
                encoding: Encoding.UTF8,
                mediaType: MimeTypeConstants.Json
            );

            return content;
        }
        #endregion
    }
}
