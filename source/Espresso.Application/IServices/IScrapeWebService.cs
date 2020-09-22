using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Domain.Enums.RssFeedEnums;

namespace Espresso.Application.IServices
{
    public interface IScrapeWebService
    {
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
