using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Models;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public class LoggerRequestPipeline<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        #region Fields
        private readonly Stopwatch _stopWatch;
        private readonly IMemoryCache _memoryCache;
        private readonly ILoggerService<LoggerRequestPipeline<TRequest, TResponse>> _loggerService;
        private readonly ApplicationInformation _applicationInformation;
        #endregion

        #region Constructors
        public LoggerRequestPipeline(
            IMemoryCache memoryCache,
            ILoggerService<LoggerRequestPipeline<TRequest, TResponse>> loggerService,
            ApplicationInformation applicationInformation
        )
        {
            _stopWatch = new Stopwatch();
            _memoryCache = memoryCache;
            _loggerService = loggerService;
            _applicationInformation = applicationInformation;
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
            var targetedApiVersion = requestBase?.TargetedApiVersion ?? "";
            var consumerVersion = requestBase?.ConsumerVersion ?? "";
            var deviceType = requestBase?.DeviceType ?? DeviceType.Undefined;

            _stopWatch.Start();
            var response = await next();
            _stopWatch.Stop();

            var duration = _stopWatch.Elapsed;
            _stopWatch.Reset();

            var averageDuration = CalculateAveragePerformance(duration: duration, requestName: requestName);

            var arguments = new List<(string argumentName, object argumentValue)>
            {
                ("Api Version", _applicationInformation.Version),
                (nameof(targetedApiVersion), targetedApiVersion),
                (nameof(consumerVersion), consumerVersion),
                (nameof(deviceType), deviceType),
                ("AppEnvironment", _applicationInformation.AppEnvironment),
                (nameof(request), request),
                (nameof(duration), duration),
                (nameof(averageDuration), averageDuration),
                (nameof(response), response!),
            };

            _loggerService.Log(requestName, LogLevel.Information, arguments);

            return response;
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
