using System;
using System.Threading;
using System.Threading.Tasks;

using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Domain.IServices
{
    public interface ILoggerService
    {
        public void LogRequest(
            int requestId,
            string requestName,
            string webApiVersion,
            string targetedWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            string requestParameters,
            TimeSpan duration,
            string responseData,
            CancellationToken cancellationToken
        );

        public Task LogRequestError(
            int requestId,
            string requestName,
            string webApiVersion,
            string targetedWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            string requestParameters,
            Exception exception,
            CancellationToken cancellationToken
        );

        public Task LogWarning(
            int eventId,
            string eventName,
            string version,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        );

        public Task LogError(
            int eventId,
            string eventName,
            string version,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        );

        public void LogWebApiMemoryCacheInit(
            int requestId,
            string requestName,
            TimeSpan duration,
            int categoriesCount,
            int newsPortalsCount,
            int articleCategoriesCount,
            int totalArticlesCount,
            int articlesCount,
            int applicationDownloadsCount
        );

        public void LogParserDeleterMemoryCacheInit(
            int requestId,
            string requestName,
            TimeSpan duration,
            int categoriesCount,
            int newsPortalsCount,
            int articleCategoriesCount,
            int totalArticlesCount,
            int articlesCount,
            int rssFeedCount
        );

        public Task LogAppDownload(
            string mobileDeviceType,
            int todayAndroidCount,
            int todayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            CancellationToken cancellationToken
        );

    }
}
