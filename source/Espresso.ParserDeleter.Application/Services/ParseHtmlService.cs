using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Extensions;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.ParserDeleter.Application.IServices;
using HtmlAgilityPack;

namespace Espresso.ParserDeleter.Application.Services
{
    public class ParseHtmlService : IParseHtmlService
    {
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

        public async Task<string?> GetImageUrlFromJsonObjectFromScriptTag(
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

        public string? GetImageUrlFromSrcAttribute(HtmlNodeCollection elementTags)
        {
            var imageUrls = elementTags.Select(imgTag => imgTag?.GetAttributeValue("src", null));
            var imageUrl = imageUrls.FirstOrDefault();

            return imageUrl;
        }
    }
}