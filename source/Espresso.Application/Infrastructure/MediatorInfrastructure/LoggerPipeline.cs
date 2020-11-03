using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public class LoggerPipeline<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        #region Fields
        private readonly Stopwatch _stopWatch;
        private readonly ISlackService _slackService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILoggerService<LoggerPipeline<TRequest, TResponse>> _loggerService;
        #endregion

        #region Constructors
        public LoggerPipeline(
            ISlackService slackService,
            IMemoryCache memoryCache,
            ILoggerService<LoggerPipeline<TRequest, TResponse>> loggerService
        )
        {
            _stopWatch = new Stopwatch();
            _slackService = slackService;
            _memoryCache = memoryCache;
            _loggerService = loggerService;
        }
        #endregion

        #region Methods
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next
        )
        {
            var requestBase = request switch
            {
                Request<TResponse> baseType => baseType,
                _ => throw new Exception($"Request:{typeof(TRequest).Name} does not implement Request abstract class!")
            };
            var requestName = typeof(TRequest).Name;
            var apiVersion = requestBase?.CurrentApiVersion ?? "";
            var targetedApiVersion = requestBase?.TargetedApiVersion ?? "";
            var consumerVersion = requestBase?.ConsumerVersion ?? "";
            var deviceType = requestBase?.DeviceType ?? DeviceType.Undefined;
            var appEnvironment = requestBase?.AppEnvironment ?? AppEnvironment.Undefined;

            try
            {
                _stopWatch.Start();
                var response = await next();
                _stopWatch.Stop();

                var duration = _stopWatch.Elapsed;
                var averageDuration = CalculateAveragePerformance(duration: duration, requestName: requestName);

                var arguments = new List<(string argumentName, object argumentValue)>
                {
                    (nameof(apiVersion), apiVersion),
                    (nameof(targetedApiVersion), targetedApiVersion),
                    (nameof(consumerVersion), consumerVersion),
                    (nameof(deviceType), deviceType),
                    (nameof(request), request),
                    (nameof(duration), duration),
                    (nameof(averageDuration), averageDuration),
                    (nameof(response), response!),
                };

                _loggerService.Log(requestName, LogLevel.Information, arguments);

                return response;
            }
            catch (Exception exception)
            {
                await LogError(
                    requestName: requestName,
                    apiVersion: apiVersion,
                    targetedApiVersion: targetedApiVersion,
                    consumerVersion: consumerVersion,
                    deviceType: deviceType,
                    request: request,
                    appEnvironment: appEnvironment,
                    exception: exception
                );

                throw;
            }
        }

        private Task LogError(
            string requestName,
            string apiVersion,
            string targetedApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            TRequest request,
            AppEnvironment appEnvironment,
            Exception exception
        )
        {
            var arguments = new List<(string argumentName, object argumentValue)>
            {
                (nameof(apiVersion), apiVersion),
                (nameof(targetedApiVersion), targetedApiVersion),
                (nameof(consumerVersion), consumerVersion),
                (nameof(deviceType), deviceType),
                (nameof(request), request),
            };

            _loggerService.Log(requestName, exception, LogLevel.Error, arguments);

            return _slackService
                .LogRequestError(
                    requestName: requestName,
                    apiVersion: apiVersion,
                    targetedApiVersion: targetedApiVersion,
                    consumerVersion: consumerVersion,
                    deviceType: deviceType,
                    requestParameters: request.ToString() ?? "",
                    exception: exception,
                    appEnvironment: appEnvironment,
                    cancellationToken: default
                );
        }

        private TimeSpan CalculateAveragePerformance(
            TimeSpan duration,
            string requestName
        )
        {
            var performanceMeasurementKey = $"{requestName}PerformanceKey";
            var (total, count) = _memoryCache.GetOrCreate(
                key: performanceMeasurementKey,
                factory: entry =>
                {
                    var total = new TimeSpan();
                    var count = 0;
                    return (total, count);
                }
            );
            total += duration;
            count++;

            _memoryCache.Set(key: performanceMeasurementKey, value: (total, count));

            return total / count;
        }
        #endregion
    }
}
