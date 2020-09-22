using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Extensions;
using Espresso.Application.IService;
using Espresso.Application.IServices;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Extensions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Services
{
    public class WebScrapingService : IWebScrapingService
    {
        #region Fields
        private readonly ILogger<WebScrapingService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IHtmlParsingService _htmlParsingService;
        #endregion

        #region Constructors
        public WebScrapingService(
            IHtmlParsingService htmlParsingService,
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory
        )
        {
            _logger = loggerFactory.CreateLogger<WebScrapingService>();
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
            _htmlParsingService = htmlParsingService;
        }
        #endregion

        #region Methods

        #region Public Methods
        public async Task<string?> GetSrcAttributeFromElementDefinedByXPath(
            string? articleUrl,
            string xPath,
            ImageUrlWebScrapeType imageUrlWebScrapeType,
            IEnumerable<string> propertyNames,
            RequestType requestType,
            CancellationToken cancellationToken
        )
        {
            if (articleUrl is null || string.IsNullOrEmpty(xPath))
            {
                return null;
            }
            var htmlString = await GetStringPageContent(
                articleUrl: articleUrl,
                requestType: requestType,
                cancellationToken: cancellationToken
            );

            if (htmlString is null)
            {
                LogWebScrapingEmptyResponse(
                    articleUrl: articleUrl,
                    requestType: requestType,
                    imageUrlWebScrapeType: imageUrlWebScrapeType
                );
                return null;
            }

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);
            var elementTags = htmlDocument.DocumentNode.SelectNodes(xPath);

            if (elementTags is null)
            {
                LogWebScrapingResponseNotBeingValidHtml(
                    articleUrl: articleUrl,
                    requestType: requestType,
                    imageUrlWebScrapeType: imageUrlWebScrapeType,
                    htmlString: htmlString
                );
                return null;
            }

            var imageUrl = imageUrlWebScrapeType switch
            {
                ImageUrlWebScrapeType.JsonObjectInScriptElement => await GetImageUrlFromJsonObjectFromScriptTag(
                    elementTags: elementTags,
                    propertyNames: propertyNames
                ),
                ImageUrlWebScrapeType.SrcAttribute => _htmlParsingService.GetImageUrlFromSrcAttribute(elementTags),
                _ => _htmlParsingService.GetImageUrlFromSrcAttribute(elementTags),
            };

            LogWebScrapeResult(
                imageUrl: imageUrl,
                articleUrl: articleUrl,
                xPath: xPath,
                requestType: requestType,
                imageUrlWebScrapeType: imageUrlWebScrapeType
            );

            return imageUrl;
        }
        #endregion

        #region Private Methods
        private static async Task<string?> GetImageUrlFromJsonObjectFromScriptTag(
            HtmlNodeCollection elementTags,
            IEnumerable<string> propertyNames
        )
        {
            var jsonText = elementTags.FirstOrDefault()?.InnerText;
            if (jsonText is null)
            {
                return null;
            }
            var jsonMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
            var data = await JsonSerializer.DeserializeAsync<JsonElement>(jsonMemoryStream);
            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string?> GetStringPageContent(
            string articleUrl,
            RequestType requestType,
            CancellationToken cancellationToken
        )
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, articleUrl);
                switch (requestType)
                {
                    case RequestType.Normal:
                    default:
                        {
                            using var response = await _httpClient.SendAsync(request: request, cancellationToken: cancellationToken);
                            _ = response.EnsureSuccessStatusCode();
                            var pageContent = await response.Content.ReadAsStringAsync();
                            return pageContent;
                        }
                    case RequestType.Browser:
                        {
                            request.AddBrowserHeadersToHttpRequestMessage();
                            using var response = await _httpClient.SendAsync(request: request, cancellationToken: cancellationToken);
                            _ = response.EnsureSuccessStatusCode();
                            using var responseStream = await response.Content.ReadAsStreamAsync();
                            using var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
                            using var streamReader = new StreamReader(decompressedStream);
                            var pageContent = await streamReader.ReadToEndAsync();
                            return pageContent;
                        }
                }
            }
            catch (Exception exception)
            {
                LogImageUrlWebScrapingRequestError(
                    exception: exception,
                    articleUrl: articleUrl,
                    requestType: requestType
                );
                return null;
            }
        }

        private void LogImageUrlWebScrapingRequestError(Exception exception, string articleUrl, RequestType requestType)
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? "";
            var eventName = Event.ImageUrlWebScrapingRequest.GetDisplayName();
            _logger.LogWarning(
                eventId: new EventId(
                    id: (int)Event.ImageUrlWebScrapingRequest,
                    name: eventName
                ),
                exception: exception,
                message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(articleUrl))}: " +
                    $"{AnsiUtility.EncodeParameter("{1}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(requestType))}: " +
                    $"{AnsiUtility.EncodeEnum("{2}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                    $"{AnsiUtility.EncodeErrorMessage("{3}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                    $"{AnsiUtility.EncodeErrorMessage("{4}")}",
                args: new object[]
                {
                        eventName,
                        articleUrl,
                        requestType,
                        exceptionMessage,
                        innerExceptionMessage
                }
            );
        }

        private void LogWebScrapingResponseNotBeingValidHtml(
            string articleUrl,
            RequestType requestType,
            ImageUrlWebScrapeType imageUrlWebScrapeType,
            string htmlString
        )
        {
            var errorMessage = "ImageUrl web scraping response is not html page!";
            var eventName = Event.ImageUrlWebScrapingData.GetDisplayName();
            _logger.LogWarning(
                eventId: new EventId(
                    id: (int)Event.ImageUrlWebScrapingData,
                    name: eventName
                ),
                message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(articleUrl))}: " +
                    $"{AnsiUtility.EncodeParameter("{1}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(requestType))}: " +
                    $"{AnsiUtility.EncodeEnum("{2}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(imageUrlWebScrapeType))}: " +
                    $"{AnsiUtility.EncodeEnum("{3}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(htmlString))}: " +
                    $"{AnsiUtility.EncodeParameter("{4}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(errorMessage))}: " +
                    $"{AnsiUtility.EncodeErrorMessage("{5}")}",
                args: new object[]
                {
                        eventName,
                        articleUrl,
                        requestType,
                        imageUrlWebScrapeType,
                        htmlString.Take(50),
                        errorMessage
                }
            );
        }

        private void LogWebScrapingEmptyResponse(
            string articleUrl,
            RequestType requestType,
            ImageUrlWebScrapeType imageUrlWebScrapeType
        )
        {
            var errorMessage = "ImageUrl web scraping response is null!";
            var eventName = Event.ImageUrlWebScrapingData.GetDisplayName();
            _logger.LogWarning(
                eventId: new EventId(
                    id: (int)Event.ImageUrlWebScrapingData,
                    name: eventName
                ),
                message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(articleUrl))}: " +
                    $"{AnsiUtility.EncodeParameter("{1}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(requestType))}: " +
                    $"{AnsiUtility.EncodeEnum("{2}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(imageUrlWebScrapeType))}: " +
                    $"{AnsiUtility.EncodeEnum("{3}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(errorMessage))}: " +
                    $"{AnsiUtility.EncodeErrorMessage("{4}")}",
                args: new object[]
                {
                        eventName,
                        articleUrl,
                        requestType,
                        imageUrlWebScrapeType,
                        errorMessage
                }
            );
        }

        private void LogWebScrapeResult(
            string? imageUrl,
            string? articleUrl,
            string xPath,
            RequestType requestType,
            ImageUrlWebScrapeType imageUrlWebScrapeType
        )
        {
            var message = "ImageUrl web scraping successful";
            var eventName = Event.ImageUrlWebScrapingData.GetDisplayName();
            _logger.LogInformation(
                eventId: new EventId(
                    id: (int)Event.ImageUrlWebScrapingData,
                    name: eventName
                ),
                message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(imageUrl))}: " +
                    $"{AnsiUtility.EncodeParameter("{1}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(articleUrl))}: " +
                    $"{AnsiUtility.EncodeParameter("{2}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(xPath))}: " +
                    $"{AnsiUtility.EncodeParameter("{3}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(requestType))}: " +
                    $"{AnsiUtility.EncodeEnum("{4}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(imageUrlWebScrapeType))}: " +
                    $"{AnsiUtility.EncodeEnum("{5}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(message))}: " +
                    $"{AnsiUtility.EncodeParameter("{6}")}",
                args: new object[]
                {
                        eventName,
                        imageUrl ?? "",
                        articleUrl ?? "",
                        xPath,
                        requestType,
                        imageUrlWebScrapeType,
                        message
                }
            );
        }
        #endregion
    }
    #endregion
}
