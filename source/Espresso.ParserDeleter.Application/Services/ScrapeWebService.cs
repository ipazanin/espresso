using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Extensions;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using Espresso.ParserDeleter.Application.IServices;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.Application.Services
{
    public class ScrapeWebService : IScrapeWebService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly IParseHtmlService _parseHtmlService;
        private readonly ILoggerService<ScrapeWebService> _loggerService;
        #endregion

        #region Constructors
        public ScrapeWebService(
            IParseHtmlService parseHtmlService,
            IHttpClientFactory httpClientFactory,
            ILoggerService<ScrapeWebService> loggerService
        )
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
            _parseHtmlService = parseHtmlService;
            _loggerService = loggerService;
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
                return null;
            }

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);
            var elementTags = htmlDocument.DocumentNode.SelectNodes(xPath);

            if (elementTags is null)
            {
                return null;
            }

            var imageUrl = imageUrlWebScrapeType switch
            {
                ImageUrlWebScrapeType.JsonObjectInScriptElement => await GetImageUrlFromJsonObjectFromScriptTag(
                    elementTags: elementTags,
                    propertyNames: propertyNames,
                    cancellationToken: cancellationToken
                ),
                ImageUrlWebScrapeType.SrcAttribute => _parseHtmlService.GetImageUrlFromSrcAttribute(elementTags),
                _ => _parseHtmlService.GetImageUrlFromSrcAttribute(elementTags),
            };

            return imageUrl;
        }
        #endregion

        #region Private Methods
        private static async Task<string?> GetImageUrlFromJsonObjectFromScriptTag(
            HtmlNodeCollection elementTags,
            IEnumerable<string> propertyNames,
            CancellationToken cancellationToken
        )
        {
            var jsonText = elementTags.FirstOrDefault()?.InnerText;
            if (jsonText is null)
            {
                return null;
            }
            var data = await JsonUtility.Deserialize<JsonElement>(jsonText, cancellationToken);
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
                            var pageContent = await response.Content.ReadAsStringAsync(cancellationToken);
                            return pageContent;
                        }
                    case RequestType.Browser:
                        {
                            request.AddBrowserHeadersToHttpRequestMessage();
                            using var response = await _httpClient.SendAsync(request: request, cancellationToken: cancellationToken);
                            _ = response.EnsureSuccessStatusCode();
                            using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
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
            var eventName = Event.ImageUrlWebScrapingRequest.GetDisplayName();
            var arguments = new (string, object)[]
            {
                (nameof(articleUrl), articleUrl),
                (nameof(requestType), requestType),
            };

            _loggerService.Log(eventName, exception, LogLevel.Error, arguments);
        }
        #endregion
    }
    #endregion
}
