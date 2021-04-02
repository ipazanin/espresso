using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;
using Espresso.Common.Enums;

namespace Espresso.Application.Services.Contracts
{
    public interface ISlackService
    {
        public Task LogError(
            string eventName,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        );

        public Task LogAppDownloadStatistics(
            int yesterdayAndroidCount,
            int yesterdayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            int activeUsers,
            decimal revenue,
            CancellationToken cancellationToken
        );

        public Task LogMissingCategoriesError(
            string rssFeedUrl,
            string articleUrl,
            string urlCategories,
            CancellationToken cancellationToken
        );

        public Task LogNewNewsPortalRequest(
            string newsPortalName,
            string email,
            string? url,
            CancellationToken cancellationToken
        );

        public Task LogYesterdaysStatistics(
            IEnumerable<Article> topArticles,
            int totalNumberOfClicks,
            IEnumerable<(NewsPortal newsPortal, int numberOfClicks, IEnumerable<Article> articles)> topNewsPortals,
            IEnumerable<(Category category, int numberOfClicks, IEnumerable<Article> articles)> categoriesWithNumberOfClicks,
            CancellationToken cancellationToken
        );

        public Task LogPushNotification(
            PushNotification pushNotification,
            Article article,
            CancellationToken cancellationToken
        );
    }
}
