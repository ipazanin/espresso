using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Domain.Enums.RssFeedEnums;

namespace Espresso.Application.IServices
{
    public interface IWebScrapingService
    {
        public string? GetText(string? html);

        public string? GetSrcAttributeFromFirstImgElement(string? html);

        public Task<string?> GetSrcAttributeFromElementDefinedByXPath(
            string? articleUrl,
            string xPath,
            ImageUrlWebScrapeType imageUrlWebScrapeType,
            IEnumerable<string> propertyNames,
            RequestType requestType,
            CancellationToken cancellationToken
        );
    }
}
