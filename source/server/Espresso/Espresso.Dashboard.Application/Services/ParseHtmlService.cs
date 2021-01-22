using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;
using Espresso.Dashboard.Application.IServices;
using HtmlAgilityPack;

namespace Espresso.Dashboard.Application.Services
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

        public string? GetImageUrlFromSrcAttribute(
            HtmlNodeCollection elementTags,
            string attributeName
        )
        {
            var imageUrls = elementTags.Select(imgTag => imgTag?.GetAttributeValue(attributeName, null));
            var imageUrl = imageUrls.FirstOrDefault();

            return imageUrl;
        }
    }
}
