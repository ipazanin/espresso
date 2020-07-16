using System;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Domain.IServices
{
    public interface ISlackService
    {
        public Task LogWarning(
            string eventName,
            string version,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        );

        public Task LogError(
            string eventName,
            string version,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        );

        public Task LogRequestError(
            string requestName,
            string webApiVersion,
            string targetedWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            string requestParameters,
            Exception exception,
            CancellationToken cancellationToken
        );

        public Task LogAppDownload(
            string mobileDeviceType,
            int todayAndroidCount,
            int todayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            CancellationToken cancellationToken
        );

        public Task LogMissingCategoriesError(
            string version,
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
    }
}
