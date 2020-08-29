using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.IServices;
using HtmlAgilityPack;

namespace Espresso.Application.DomainServices
{
    public class WebScrapingService : IWebScrapingService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructors
        public WebScrapingService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }
        #endregion

        #region Methods

        #region Public Methods
        public async Task<string?> GetSrcAttributeFromElementDefinedByXPath(
            string? articleUrl,
            string xPath,
            ImageUrlWebScrapeType imageUrlWebScrapeType,
            IEnumerable<string> propertyNames,
            CancellationToken cancellationToken
        )
        {
            if (articleUrl is null || string.IsNullOrEmpty(xPath))
            {
                return null;
            }
            var response = await _httpClient.GetAsync(articleUrl, cancellationToken).ConfigureAwait(false);
            var htmlString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);
            var elementTags = htmlDocument.DocumentNode.SelectNodes(xPath);

            return elementTags == null
                ? null
                : (imageUrlWebScrapeType switch
                {
                    ImageUrlWebScrapeType.JsonObjectInScriptElement => await GetImageUrlFromJsonObjectFromScriptTag(
                        elementTags: elementTags,
                        propertyNames: propertyNames
                    ).ConfigureAwait(false),
                    ImageUrlWebScrapeType.Undefined => GetImageUrlFromSrcAttribute(elementTags),
                    ImageUrlWebScrapeType.SrcAttribute => GetImageUrlFromSrcAttribute(elementTags),
                    _ => GetImageUrlFromSrcAttribute(elementTags),
                });

        }

        public string? GetSrcAttributeFromFirstImgElement(string? html)
        {
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
        private async Task<string?> GetImageUrlFromJsonObjectFromScriptTag(
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
            var data = await JsonSerializer.DeserializeAsync<JsonElement>(jsonMemoryStream).ConfigureAwait(false);
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

        private string? GetImageUrlFromSrcAttribute(HtmlNodeCollection elementTags)
        {
            var imageUrls = elementTags.Select(imgTag => imgTag?.GetAttributeValue("src", null));
            var imageUrl = imageUrls.FirstOrDefault();

            return imageUrl;
        }
        #endregion
    }
    #endregion
}
