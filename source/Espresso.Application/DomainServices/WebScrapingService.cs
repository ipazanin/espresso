using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.DomainServices
{
    public class WebScrapingService : IWebScrapingService
    {
        #region Fields
        private readonly ILogger<WebScrapingService> _logger;
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructors
        public WebScrapingService(
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory
        )
        {
            _logger = loggerFactory.CreateLogger<WebScrapingService>();
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
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
                ImageUrlWebScrapeType.SrcAttribute => GetImageUrlFromSrcAttribute(elementTags),
                _ => GetImageUrlFromSrcAttribute(elementTags),
            };

            return imageUrl;
        }

        public string? GetSrcAttributeFromFirstImgElement(string? html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return null;
            }

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var imgTag = htmlDocument.DocumentNode.SelectSingleNode("//img");

            var srcAttributeValue = imgTag?.GetAttributeValue("src", null);

            return srcAttributeValue;
        }

        public string? GetText(string? html)
        {
            if (html is null)
            {
                return null;
            }

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var nodes = htmlDocument.DocumentNode
                .SelectNodes(".//text()")
                ?.Select(node => node?.InnerText) ?? new List<string>();

            var summary = HtmlEntity.DeEntitize(string.Join(" ", nodes));
            summary = Regex.Replace(summary, @"\r\n?|\n", " ").RemoveExtraWhiteSpaceCharacters();

            if (string.IsNullOrWhiteSpace(summary))
            {
                return null;
            }

            if (summary.Length > Article.SummaryMaxLength)
            {
                summary = summary.Replace(@"\n", " ");
                summary = $"{string.Concat(summary.Take(Article.SummaryMaxLength - 4))}...";
            }

            return summary;
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

        private static string? GetImageUrlFromSrcAttribute(HtmlNodeCollection elementTags)
        {
            var imageUrls = elementTags.Select(imgTag => imgTag?.GetAttributeValue("src", null));
            var imageUrl = imageUrls.FirstOrDefault();

            return imageUrl;
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
                            request.Headers.TryAddWithoutValidation("accept", "*/*");
                            request.Headers.TryAddWithoutValidation("accept-encoding", "gzip, deflate");
                            request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Mobile Safari/537.36");
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
                throw;
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
        #endregion
    }
    #endregion
}
