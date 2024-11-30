// SendInformationToApiHttpService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Application.Extensions;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Common.Services.Contacts;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.IServices;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.Services;

public class SendInformationToApiHttpService : ISendInformationToApiService
{
    private readonly ILoggerService<SendInformationToApiHttpService> _loggerService;
    private readonly ISlackService _slackService;
    private readonly IJsonService _jsonService;
    private readonly string _currentVersion;
    private readonly string _serverUrl;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendInformationToApiHttpService"/> class.
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="loggerService"></param>
    /// <param name="slackService"></param>
    /// <param name="jsonService"></param>
    /// <param name="parserApiKey"></param>
    /// <param name="targetedApiVersion"></param>
    /// <param name="currentVersion"></param>
    /// <param name="serverUrl"></param>
    public SendInformationToApiHttpService(
        IHttpClientFactory httpClientFactory,
        ILoggerService<SendInformationToApiHttpService> loggerService,
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
        _currentVersion = currentVersion;
        _serverUrl = serverUrl;
        _httpClient = httpClientFactory.CreateClient(HttpClientConstants.SendArticlesHttpClientName);

        var httpHeaders = new List<(string headerKey, string headerValue)>
        {
            (headerKey: HttpHeaderConstants.ApiKeyHeaderName, headerValue: parserApiKey),
            (headerKey: HttpHeaderConstants.ApiVersionHeaderName, headerValue: targetedApiVersion),
            (headerKey: HttpHeaderConstants.VersionHeaderName, headerValue: currentVersion),
            (headerKey: HttpHeaderConstants.DeviceTypeHeaderName, headerValue: DeviceType.RssFeedParser.GetIntegerValueAsString()),
        };
        _ = _httpClient.AddHeadersToHttpClient(httpHeaders);
    }

    public async Task SendSettingUpdatedNotification()
    {
        try
        {
            using var response = await _httpClient.PostAsync(
                requestUri: $"{_serverUrl}/api/notifications/setting",
                content: null);

            _ = response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            const string EventName = "Setting Updated API Notification";
            var version = _currentVersion;
            var arguments = new (string parameterName, object parameterValue)[]
            {
                (nameof(version), version),
            };

            _loggerService.Log(EventName, exception, LogLevel.Error, arguments);

            await _slackService.LogError(
                    eventName: EventName,
                    message: exception.Message,
                    exception: exception,
                    cancellationToken: default);
        }
    }

    public async Task SendArticlesMessage(
        IEnumerable<Guid> createArticleIds,
        IEnumerable<Guid> updateArticleIds)
    {
        if (!createArticleIds.Any() && !updateArticleIds.Any())
        {
            return;
        }

        var data = new ArticlesBodyDto(
            createdArticleIds: createArticleIds,
            updatedArticleIds: updateArticleIds);

        var httpContent = await _jsonService.GetJsonHttpContent(
            value: data,
            cancellationToken: default);

        try
        {
            using var response = await _httpClient.PostAsync(
                requestUri: $"{_serverUrl}/api/notifications/articles",
                content: httpContent);

            _ = response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            var eventName = LoggingEvent.SendNewAndUpdatedArticlesRequest.GetDisplayName();
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
                    cancellationToken: default);
        }
    }

    public async Task SendCacheUpdatedNotification()
    {
        try
        {
            using var response = await _httpClient.PostAsync(
                requestUri: $"{_serverUrl}/api/notifications/cache",
                content: null);

            _ = response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            const string EventName = "Cache Updated API Notification";
            var version = _currentVersion;
            var arguments = new (string parameterName, object parameterValue)[]
            {
                (nameof(version), version),
            };

            _loggerService.Log(EventName, exception, LogLevel.Error, arguments);

            await _slackService.LogError(
                    eventName: EventName,
                    message: exception.Message,
                    exception: exception,
                    cancellationToken: default);
        }
    }
}
