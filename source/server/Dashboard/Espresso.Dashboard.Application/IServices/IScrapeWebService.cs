// IScrapeWebService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

namespace Espresso.Dashboard.Application.IServices
{
    public interface IScrapeWebService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="articleUrl"></param>
        /// <param name="requestType"></param>
        /// <param name="imageUrlParseConfiguration"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<string?> GetSrcAttributeFromElementDefinedByXPath(
            string? articleUrl,
            RequestType requestType,
            ImageUrlParseConfiguration imageUrlParseConfiguration,
            CancellationToken cancellationToken);
    }
}
