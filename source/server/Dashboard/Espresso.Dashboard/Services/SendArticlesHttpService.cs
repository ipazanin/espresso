// SendArticlesHttpService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Application.Extensions;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Common.Services.Contracts;
using Espresso.Dashboard.Application.Constants;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.Services;

public class SendArticlesHttpService : ISendArticlesService
{
    private readonly ILoggerService<SendArticlesHttpService> _loggerService;
    private readonly ISlackService _slackService;
    private readonly IJsonService _jsonService;
    private readonly string _parserApiKey;
    private readonly string _targetedApiVersion;
    private readonly string _currentVersion;
    private readonly string _serverUrl;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendArticlesHttpService"/> class.
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="loggerService"></param>
    /// <param name="slackService"></param>
    /// <param name="jsonService"></param>
    /// <param name="parserApiKey"></param>
    /// <param name="targetedApiVersion"></param>
    /// <param name="currentVersion"></param>
    /// <param name="serverUrl"></param>
    public SendArticlesHttpService(
        IHttpClientFactory httpClientFactory,
        ILoggerService<SendArticlesHttpService> loggerService,
        ISlackService slackService,
        IJsonService jsonService,
        string parserApiKey,
        string targetedApiVersion,
        string currentVersion,
        string serverUrl)
    {
        _loggerService = loggerService;
        _slackService = slackService;
        _jsonService = jsonService;
        _parserApiKey = parserApiKey;
        _targetedApiVersion = targetedApiVersion;
        _currentVersion = currentVersion;
        _serverUrl = serverUrl;
        _httpClient = httpClientFactory.CreateClient(HttpClientConstants.SendArticlesHttpClientName);
    }

    public async Task SendArticlesMessage(
        IEnumerable<Article> createArticles,
        IEnumerable<Article> updateArticles,
        CancellationToken cancellationToken)
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
            updatedArticles: updateArticles.Select(articleDtoProjection));

        var httpContent = await _jsonService.GetJsonHttpContent(
            value: data,
            cancellationToken: cancellationToken);

        try
        {
            _httpClient.AddHeadersToHttpClient(httpHeaders);
            var response = await _httpClient.PostAsync(
                requestUri: $"{_serverUrl}/api/notifications/articles",
                content: httpContent,
                cancellationToken: cancellationToken);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            var eventName = Event.SendNewAndUpdatedArticlesRequest.GetDisplayName();
            var version = _currentVersion;
            var arguments = new (string parameterName, object parameterValue)[]
            {
                    (nameof(version), version),
            };

            _loggerService.Log(eventName, exception, LogLevel.Error, arguments);

            await _slackService.LogError(
                    eventName: eventName,
                    message: exception.Message,
                    exception: exception,
                    cancellationToken: cancellationToken);
        }
    }
}
