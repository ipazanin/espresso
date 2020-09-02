using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Extensions;
using Espresso.Common.Constants;
using Espresso.Domain.IServices;

namespace Espresso.Application.DomainServices
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
        public async Task PostJsonAsync<TData>(
            string url,
            TData data,
            IEnumerable<(string headerName, string headerValue)>? httpHeaders,
            TimeSpan httpClientTimeout,
            CancellationToken cancellationToken
        )
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = httpClientTimeout;
            _ = httpClient.AddHeadersToHttpClient(httpHeaders);

            var httpContent = await CreateJsonHttpContent(
                data: data,
                cancellationToken: cancellationToken
            );
            var response = await httpClient
                .PostAsync(
                    requestUri: url,
                    content: httpContent
                )
                ;

            var statusCode = response.StatusCode;

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response
                    .Content
                    .ReadAsStringAsync();

                throw new Exception(
                    message: $"Request failed with {nameof(statusCode)}:{statusCode} " +
                        $"and {nameof(responseMessage)}:{responseMessage}"
                );
            }
        }

        private async Task<HttpContent> CreateJsonHttpContent<TData>(
            TData data,
            CancellationToken cancellationToken
        )
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(
                utf8Json: stream,
                value: data,
                options: null,
                cancellationToken: cancellationToken
            );

            stream.Position = 0;
            using var reader = new StreamReader(stream);
            var jsonString = await reader
                .ReadToEndAsync()
                ;

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
