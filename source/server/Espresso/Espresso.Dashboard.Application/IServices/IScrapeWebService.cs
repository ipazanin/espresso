using System.Threading;
using System.Threading.Tasks;

using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

namespace Espresso.Dashboard.Application.IServices
{
    public interface IScrapeWebService
    {
        public Task<string?> GetSrcAttributeFromElementDefinedByXPath(
            string? articleUrl,
            RequestType requestType,
            ImageUrlParseConfiguration imageUrlParseConfiguration,
            CancellationToken cancellationToken
        );
    }
}
