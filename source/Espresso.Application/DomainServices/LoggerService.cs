using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Configuration;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Utilities;
using Espresso.Domain.Extensions;
using Espresso.Common.Enums;

namespace Espresso.Application.DomainServices
{
    public class LoggerService : ILoggerService
    {
        #region Fields
        private readonly ISlackService _slackService;
        private readonly ICommonConfiguration _commonConfiguration;
        private readonly ILogger<LoggerService> _logger;
        #endregion

        #region Constructors
        public LoggerService(
            ILoggerFactory loggerFactory,
            ISlackService slackService,
            ICommonConfiguration commonConfiguration
        )
        {
            _slackService = slackService;
            _commonConfiguration = commonConfiguration;
            _logger = loggerFactory.CreateLogger<LoggerService>();
        }
        #endregion

        #region Methods
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
        )
        {
            var message =
                $"{AnsiUtility.EncodeRequestName("{0}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(webApiVersion))}: " +
                $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(targetedWebApiVersion))}: " +
                $"{AnsiUtility.EncodeVersion("{2}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(consumerVersion))}: " +
                $"{AnsiUtility.EncodeVersion("{3}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(deviceType))}: " +
                $"{AnsiUtility.EncodeDeviceType("{4}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(requestParameters))}: " +
                $"{AnsiUtility.EncodeRequestParameters("{5}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(duration))}: " +
                $"{AnsiUtility.EncodeDuration("{6}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(responseData))}: " +
                $"{AnsiUtility.EncodeResponse("{7}")}";

            var args = new object[]
            {
                requestName,
                webApiVersion,
                targetedWebApiVersion,
                consumerVersion,
                deviceType.GetDisplayName(),
                requestParameters,
                duration,
                responseData
            };

            _logger.LogInformation(
                eventId: new EventId(id: requestId, name: requestName),
                message: message,
                args: args
            );
        }

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
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;

            var message =
                $"{AnsiUtility.EncodeRequestName("{0}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(webApiVersion))}: " +
                $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(targetedWebApiVersion))}: " +
                $"{AnsiUtility.EncodeVersion("{2}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(consumerVersion))}: " +
                $"{AnsiUtility.EncodeVersion("{3}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(deviceType))}: " +
                $"{AnsiUtility.EncodeDeviceType("{4}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(requestParameters))}: " +
                $"{AnsiUtility.EncodeRequestParameters("{5}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                $"{AnsiUtility.EncodeErrorMessage("{6}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                $"{AnsiUtility.EncodeErrorMessage("{7}")}";

            var args = new object[]
            {
                requestName,
                webApiVersion,
                targetedWebApiVersion,
                consumerVersion,
                deviceType.GetDisplayName(),
                requestParameters,
                exceptionMessage,
                innerExceptionMessage
            };

            _logger.LogError(
                eventId: new EventId(id: requestId, name: requestName),
                exception: exception,
                message: message,
                args: args
            );

            return _commonConfiguration.AppEnvironment switch
            {
                AppEnvironment.Production => _slackService.LogRequestError(
                    requestName: requestName,
                    webApiVersion: webApiVersion,
                    targetedWebApiVersion: targetedWebApiVersion,
                    consumerVersion: consumerVersion,
                    deviceType: deviceType,
                    requestParameters: requestParameters,
                    exception: exception,
                    cancellationToken: cancellationToken
                ),
                _ => Task.CompletedTask,
            };
        }

        public Task LogWarning(
            int eventId,
            string eventName,
            string version,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;

            var formattedMessage =
                $"{AnsiUtility.EncodeRequestName("{0}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(message))}: " +
                $"{AnsiUtility.EncodeRequestParameters("{1}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                $"{AnsiUtility.EncodeErrorMessage("{3}")}";

            var args = new object[]
            {
                eventName,
                message,
                exceptionMessage,
                innerExceptionMessage,
            };

            switch (_commonConfiguration.AppEnvironment)
            {
                case AppEnvironment.Undefined:
                default:
                case AppEnvironment.Local:
                case AppEnvironment.Development:
                    _logger.LogWarning(
                        eventId: new EventId(id: eventId, name: eventName),
                        message: formattedMessage,
                        args: args
                    );
                    return Task.CompletedTask;
                case AppEnvironment.Production:
                    return _slackService.LogWarning(
                        eventName: eventName,
                        version: version,
                        message: message,
                        exception: exception,
                        cancellationToken: cancellationToken
                    );
            }
        }

        public Task LogError(
            int eventId,
            string eventName,
            string version,
            string message,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;

            var formattedMessage =
                $"{AnsiUtility.EncodeRequestName("{0}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(version))}: " +
                $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                $"{AnsiUtility.EncodeErrorMessage("{3}")}";

            var args = new object[]
            {
                eventName,
                version,
                exceptionMessage,
                innerExceptionMessage,
            };

            _logger.LogError(
                eventId: new EventId(
                    id: eventId,
                    name: eventName
                ),
                exception: exception,
                message: formattedMessage,
                args: args
            );

            return _commonConfiguration.AppEnvironment switch
            {
                AppEnvironment.Production => _slackService.LogError(
                    eventName: eventName,
                    version: version,
                    message: message,
                    exception: exception,
                    cancellationToken: cancellationToken
                ),
                _ => Task.CompletedTask,
            };
        }

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
        )
        {
            var eventId = new EventId(id: requestId, name: requestName);
            var message =
                $"{AnsiUtility.EncodeRequestName($"{{@{nameof(requestName)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(duration))}: " +
                $"{AnsiUtility.EncodeDuration($"{{@{nameof(duration)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(categoriesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(categoriesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(newsPortalsCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(newsPortalsCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(articleCategoriesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(articleCategoriesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(totalArticlesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(totalArticlesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(articlesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(articlesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(applicationDownloadsCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(applicationDownloadsCount)}}}")}\n\t";

            var args = new object[]
            {
                requestName,
                duration,
                categoriesCount,
                newsPortalsCount,
                articleCategoriesCount,
                totalArticlesCount,
                articlesCount,
                applicationDownloadsCount
            };

            _logger.LogInformation(
                eventId: eventId,
                message: message,
                args: args
            );
        }

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
        )
        {
            var eventId = new EventId(id: requestId, name: requestName);
            var message =
                $"{AnsiUtility.EncodeRequestName($"{{@{nameof(requestName)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(duration))}: " +
                $"{AnsiUtility.EncodeDuration($"{{@{nameof(duration)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(categoriesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(categoriesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(newsPortalsCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(newsPortalsCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(articleCategoriesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(articleCategoriesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(totalArticlesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(totalArticlesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(articlesCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(articlesCount)}}}")}\n\t" +
                $"{AnsiUtility.EncodeParameterName(nameof(rssFeedCount))}: " +
                $"{AnsiUtility.EncodeRequestParameters($"{{@{nameof(rssFeedCount)}}}")}\n\t";

            var args = new object[]
            {
                requestName,
                duration,
                categoriesCount,
                newsPortalsCount,
                articleCategoriesCount,
                totalArticlesCount,
                articlesCount,
                rssFeedCount
            };

            _logger.LogInformation(
                eventId: eventId,
                message: message,
                args: args
            );
        }

        public Task LogAppDownload(
            string mobileDeviceType,
            int todayAndroidCount,
            int todayIosCount,
            int totalAndroidCount,
            int totalIosCount,
            CancellationToken cancellationToken
        )
        {

            return _commonConfiguration.AppEnvironment switch
            {
                AppEnvironment.Production => _slackService.LogAppDownload(
                    mobileDeviceType: mobileDeviceType,
                    todayAndroidCount: todayAndroidCount,
                    todayIosCount: todayIosCount,
                    totalAndroidCount: totalAndroidCount,
                    totalIosCount: totalIosCount,
                    cancellationToken: cancellationToken
                ),
                _ => Task.CompletedTask,

            };
        }
        #endregion
    }
}
