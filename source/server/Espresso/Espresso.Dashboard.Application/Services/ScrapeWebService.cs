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
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Common.Extensions;
using Espresso.Domain.IServices;
using Espresso.Dashboard.Application.IServices;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Espresso.Common.Services.Contracts;
using Espresso.Dashboard.Application.Constants;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

namespace Espresso.Dashboard.Application.Services
{
    public class ScrapeWebService : IScrapeWebService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly IParseHtmlService _parseHtmlService;
        private readonly ILoggerService<ScrapeWebService> _loggerService;
        private readonly IJsonService _jsonService;
        #endregion

        #region Constructors
        public ScrapeWebService(
            IParseHtmlService parseHtmlService,
            IHttpClientFactory httpClientFactory,
            ILoggerService<ScrapeWebService> loggerService,
            IJsonService jsonService
        )
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientConstants.ScrapeWebHttpClientName);
            _parseHtmlService = parseHtmlService;
            _loggerService = loggerService;
            _jsonService = jsonService;
        }
        #endregion

        #region Methods

        #region Public Methods
        public async Task<string?> GetSrcAttributeFromElementDefinedByXPath(
            string? articleUrl,
            RequestType requestType,
            ImageUrlParseConfiguration imageUrlParseConfiguration,
            CancellationToken cancellationToken
        )
        {
            if (articleUrl is null || string.IsNullOrEmpty(imageUrlParseConfiguration.XPath))
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
            var elementTags = htmlDocument.DocumentNode.SelectNodes(imageUrlParseConfiguration.XPath);

            if (elementTags is null)
            {
                return null;
            }

            var imageUrl = imageUrlParseConfiguration.ImageUrlWebScrapeType switch
            {
                ImageUrlWebScrapeType.JsonObjectInScriptElement => await GetImageUrlFromJsonObjectFromScriptTag(
                    elementTags: elementTags,
                    propertyNames: imageUrlParseConfiguration.GetPropertyNames(),
                    cancellationToken: cancellationToken
                ),
                _ => _parseHtmlService.GetImageUrlFromSrcAttribute(
                    elementTags: elementTags,
                    attributeName: imageUrlParseConfiguration.AttributeName
                ),
            };

            return imageUrl;
        }
        #endregion

        #region Private Methods
        private async Task<string?> GetImageUrlFromJsonObjectFromScriptTag(
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
                        (nameof(propertyNames), string.Join(", ", propertyNames))
                    }
                );
                return null;
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
