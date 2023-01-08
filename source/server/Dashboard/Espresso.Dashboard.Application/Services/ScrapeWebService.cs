// ScrapeWebService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects;
using Espresso.Application.Extensions;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Common.Services.Contracts;
using Espresso.Dashboard.Application.Constants;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.IServices;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace Espresso.Dashboard.Application.Services;

public class ScrapeWebService : IScrapeWebService
{
    private readonly HttpClient _httpClient;
    private readonly IParseHtmlService _parseHtmlService;
    private readonly ILoggerService<ScrapeWebService> _loggerService;
    private readonly IJsonService _jsonService;
    private readonly IParsingMessagesService _parsingMessagesService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScrapeWebService"/> class.
    /// </summary>
    /// <param name="parseHtmlService"></param>
    /// <param name="httpClientFactory"></param>
    /// <param name="loggerService"></param>
    /// <param name="jsonService"></param>
    /// <param name="parsingMessagesService"></param>
    public ScrapeWebService(
        IParseHtmlService parseHtmlService,
        IHttpClientFactory httpClientFactory,
        ILoggerService<ScrapeWebService> loggerService,
        IJsonService jsonService,
        IParsingMessagesService parsingMessagesService)
    {
        _httpClient = httpClientFactory.CreateClient(HttpClientConstants.ScrapeWebHttpClientName);
        _parseHtmlService = parseHtmlService;
        _loggerService = loggerService;
        _jsonService = jsonService;
        _parsingMessagesService = parsingMessagesService;
    }

    public async Task<string?> GetSrcAttributeFromElementDefinedByXPath(
        string? articleUrl,
        RssFeed rssFeed,
        CancellationToken cancellationToken)
    {
        if (articleUrl is null || string.IsNullOrEmpty(rssFeed.ImageUrlParseConfiguration.XPath))
        {
            return null;
        }

        var htmlString = await GetStringPageContent(
            articleUrl: articleUrl,
            requestType: rssFeed.ImageUrlParseConfiguration.WebScrapeRequestType,
            rssFeed: rssFeed,
            cancellationToken: cancellationToken);

        if (htmlString is null)
        {
            return null;
        }

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(htmlString);
        var elementTags = htmlDocument.DocumentNode.SelectNodes(rssFeed.ImageUrlParseConfiguration.XPath);

        if (elementTags is null)
        {
            var parsingErrorMessage = new ParsingErrorMessageDto(
                logLevel: LogLevel.Warning,
                message: $"No HTML element found with xPath: {rssFeed.ImageUrlParseConfiguration.XPath}",
                rssFeedId: rssFeed.Id);
            _parsingMessagesService.PushMessage(parsingErrorMessage);
            return null;
        }

        var imageUrl = rssFeed.ImageUrlParseConfiguration.ImageUrlWebScrapeType switch
        {
            ImageUrlWebScrapeType.JsonObjectInScriptElement => await GetImageUrlFromJsonObjectFromScriptTag(
                elementTags: elementTags,
                propertyNames: rssFeed.ImageUrlParseConfiguration.GetPropertyNames(),
                cancellationToken: cancellationToken),
            _ => _parseHtmlService.GetImageUrlFromAttribute(
                elementTags: elementTags,
                attributeName: rssFeed.ImageUrlParseConfiguration.AttributeName),
        };

        return imageUrl;
    }

    private async Task<string?> GetImageUrlFromJsonObjectFromScriptTag(
        HtmlNodeCollection elementTags,
        IEnumerable<string> propertyNames,
        CancellationToken cancellationToken)
    {
        var jsonText = elementTags.FirstOrDefault()?.InnerText;
        if (jsonText is null)
        {
            return null;
        }

        try
        {
            var data = await _jsonService.Deserialize<JsonElement>(jsonText, cancellationToken);
            JsonElement property = default;
            var count = 0;
            foreach (var propertyName in propertyNames)
            {
                property =
                    count++ == 0 ?
                    data.GetProperty(propertyName) :
                    property.GetProperty(propertyName);
            }

            if (property.Equals(default))
            {
                return null;
            }

            var imageUrl = property.GetString();

            return imageUrl;
        }
        catch (Exception exception)
        {
            _loggerService.Log(
                eventName: "GetImageUrlFromJsonObjectFromScriptTag Error while parsing JSON",
                exception: exception,
                logLevel: LogLevel.Warning,
                namedArguments: new (string, object)[]
                {
                        (nameof(jsonText), jsonText),
                        (nameof(propertyNames), string.Join(", ", propertyNames)),
                });
            return null;
        }
    }

    private async Task<string?> GetStringPageContent(
        string articleUrl,
        RequestType requestType,
        RssFeed rssFeed,
        CancellationToken cancellationToken)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, articleUrl);
            switch (requestType)
            {
                default:
                    {
                        using var response = await _httpClient.SendAsync(request: request, cancellationToken: cancellationToken);
                        response.EnsureSuccessStatusCode();
                        var pageContent = await response.Content.ReadAsStringAsync(cancellationToken);
                        return pageContent;
                    }

                case RequestType.Browser:
                    {
                        request.AddBrowserHeadersToHttpRequestMessage();
                        using var response = await _httpClient.SendAsync(request: request, cancellationToken: cancellationToken);
                        response.EnsureSuccessStatusCode();
                        using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                        using var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
                        using var streamReader = new StreamReader(decompressedStream);
                        var pageContent = await streamReader.ReadToEndAsync(cancellationToken);
                        return pageContent;
                    }
            }
        }
        catch (Exception exception)
        {
            LogImageUrlWebScrapingRequestError(
                exception: exception,
                articleUrl: articleUrl,
                requestType: requestType);

            var parsingErrorMessage = new ParsingErrorMessageDto(
                logLevel: LogLevel.Warning,
                message: $"Scraping Exception while downloading web page: {exception}",
                rssFeedId: rssFeed.Id);
            _parsingMessagesService.PushMessage(parsingErrorMessage);
            return null;
        }
    }

    private void LogImageUrlWebScrapingRequestError(Exception exception, string articleUrl, RequestType requestType)
    {
        var eventName = Event.ImageUrlWebScrapingRequest.GetDisplayName();
        var arguments = new (string, object)[]
        {
                (nameof(articleUrl), articleUrl),
                (nameof(requestType), requestType),
        };

        _loggerService.Log(eventName, exception, LogLevel.Error, arguments);
    }
}
