using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Enums;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.IServices
{
    public interface ISlackService
    {
        public Task LogError(
            string eventName,
            string version,
            string message,
            Exception exception,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        );

        public Task LogRequestError(
            string requestName,
            string apiVersion,
            string targetedApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            string requestParameters,
            Exception exception,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        );

        public Task LogAppDownloadStatistics(
            int yesterdayAndroidCount,
            int yesterdayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        );

        public Task LogMissingCategoriesError(
            string version,
            string rssFeedUrl,
            string articleUrl,
            string urlCategories,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        );

        public Task LogNewNewsPortalRequest(
            string newsPortalName,
            string email,
            string? url,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        );

        public Task LogYesterdaysStatistics(
            IEnumerable<Article> topArticles,
            int totalNumberOfClicks,
            IEnumerable<(NewsPortal newsPortal, int numberOfClicks)> topNewsPortals,
            IEnumerable<(Category category, int numberOfClicks)> categoriesWithNumberOfClicks,
            AppEnvironment appEnvironment,
            CancellationToken cancellationToken
        );
    }
}
