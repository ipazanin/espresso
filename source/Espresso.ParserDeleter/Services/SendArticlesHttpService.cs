using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Application.Extensions;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
using Espresso.ParserDeleter.Application.IServices;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.Services
{
    public class SendArticlesHttpService : ISendArticlesService
    {
        private readonly ILoggerService<SendArticlesHttpService> _loggerService;
        private readonly ISlackService _slackService;
        private readonly string _parserApiKey;
        private readonly string _targetedApiVersion;
        private readonly string _currentVersion;
        private readonly string _serverUrl;
        private readonly HttpClient _httpClient;
        #region Fields
        #endregion

        #region Constructors
        public SendArticlesHttpService(
            IHttpClientFactory httpClientFactory,
            ILoggerService<SendArticlesHttpService> loggerService,
            ISlackService slackService,
            string parserApiKey,
            string targetedApiVersion,
            string currentVersion,
            string serverUrl
        )
        {
            _loggerService = loggerService;
            _slackService = slackService;
            _parserApiKey = parserApiKey;
            _targetedApiVersion = targetedApiVersion;
            _currentVersion = currentVersion;
            _serverUrl = serverUrl;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(4);
        }
        #endregion

        #region Methods
        public async Task SendArticlesMessage(
            IEnumerable<Article> createArticles,
            IEnumerable<Article> updateArticles,
            CancellationToken cancellationToken
        )
        {
            if (!createArticles.Any() && !updateArticles.Any())
            {
                return;
            }

            var httpHeaders = new List<(string headerKey, string headerValue)>
            {
                (headerKey: HttpHeaderConstants.ApiKeyHeaderName, headerValue: _parserApiKey),
                (headerKey: HttpHeaderConstants.ApiVersionHeaderName, headerValue: _targetedApiVersion),
                (headerKey: HttpHeaderConstants.VersionHeaderName, headerValue: _currentVersion),
                (headerKey: HttpHeaderConstants.DeviceTypeHeaderName, headerValue: DeviceType.RssFeedParser.GetIntegerValueAsString()),
            };

            var articleDtoProjection = ArticleDto.GetProjection().Compile();

            var data = new ArticlesBodyDto(
                createdArticles: createArticles.Select(articleDtoProjection),
                updatedArticles: updateArticles.Select(articleDtoProjection)
            );

            try
            {
                _httpClient.AddHeadersToHttpClient(httpHeaders);
                var response = await _httpClient.PostAsJsonAsync(
                    requestUri: $"{_serverUrl}/api/notifications/articles",
                    value: data,
                    cancellationToken: cancellationToken
                );

                response.EnsureSuccessStatusCode();

                return;
            }
            catch (Exception exception)
            {
                var eventName = Event.SendNewAndUpdatedArticlesRequest.GetDisplayName();
                var version = _currentVersion;
                var arguments = new (string parameterName, object parameterValue)[]
                {
                    (nameof(version), version)
                };

                _loggerService.Log(eventName, exception, LogLevel.Error, arguments);

                await _slackService.LogError(
                        eventName: eventName,
                        message: exception.Message,
                        exception: exception,
                        cancellationToken: cancellationToken
                );
            }
        }
        #endregion
    }
}