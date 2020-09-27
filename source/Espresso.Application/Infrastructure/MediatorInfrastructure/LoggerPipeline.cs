using System;
using System.Diagnostics;
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
        private readonly ILogger<LoggerPipeline<TRequest, TResponse>> _logger;
        #endregion

        #region Constructors
        public LoggerPipeline(
            ISlackService slackService,
            ILoggerFactory loggerFactory,
            IMemoryCache memoryCache
        )
        {
            _stopWatch = new Stopwatch();
            _slackService = slackService;
            _memoryCache = memoryCache;
            _logger = loggerFactory.CreateLogger<LoggerPipeline<TRequest, TResponse>>();
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
            var requestId = (int)(requestBase?.EventIdEnum ?? Event.Undefined);
            var requestName = typeof(TRequest).Name;
            var apiVersion = requestBase?.CurrentApiVersion ?? "";
            var targetedApiVersion = requestBase?.TargetedApiVersion ?? "";
            var consumerVersion = requestBase?.ConsumerVersion ?? "";
            var deviceType = requestBase?.DeviceType ?? DeviceType.Undefined;
            var requestParameters = request?.ToString() ?? "";
            var appEnvironment = requestBase?.AppEnvironment ?? AppEnvironment.Undefined;

            try
            {
                _stopWatch.Start();
                var response = await next();
                _stopWatch.Stop();

                var duration = _stopWatch.Elapsed;
                var responseData = response?.ToString() ?? "";
                var averageDuration = CalculateAveragePerformance(duration: duration, requestName: requestName);


                _logger.LogInformation(
                    eventId: new EventId(id: requestId, name: requestName),
                    message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(apiVersion))}: " +
                        $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(targetedApiVersion))}: " +
                        $"{AnsiUtility.EncodeVersion("{2}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(consumerVersion))}: " +
                        $"{AnsiUtility.EncodeVersion("{3}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(deviceType))}: " +
                        $"{AnsiUtility.EncodeEnum("{4}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(requestParameters))}: " +
                        $"{AnsiUtility.EncodeRequestParameters("{5}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(duration))}: " +
                        $"{AnsiUtility.EncodeTimespan("{6}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(averageDuration))}: " +
                        $"{AnsiUtility.EncodeTimespan("{7}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(responseData))}: " +
                        $"{AnsiUtility.EncodeResponse("{8}")}",
                    args: new object[]
                    {
                        requestName,
                        apiVersion,
                        targetedApiVersion,
                        consumerVersion,
                        deviceType.GetDisplayName(),
                        requestParameters,
                        duration,
                        averageDuration,
                        responseData
                    }
                );
                return response;
            }
            catch (Exception exception)
            {
                await LogError(
                    requestId: requestId,
                    requestName: requestName,
                    apiVersion: apiVersion,
                    targetedApiVersion: targetedApiVersion,
                    consumerVersion: consumerVersion,
                    deviceType: deviceType,
                    requestParameters: requestParameters,
                    appEnvironment: appEnvironment,
                    exception: exception
                );

                throw;
            }
        }

        private Task LogError(
            int requestId,
            string requestName,
            string apiVersion,
            string targetedApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            string requestParameters,
            AppEnvironment appEnvironment,
            Exception exception
        )
        {
            var exceptionMessage = exception.Message;
            var innerExceptionMessage = exception.InnerException?.Message ?? "";

            _logger.LogError(
                eventId: new EventId(id: requestId, name: requestName),
                exception: exception,
                message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(apiVersion))}: " +
                    $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(targetedApiVersion))}: " +
                    $"{AnsiUtility.EncodeVersion("{2}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(consumerVersion))}: " +
                    $"{AnsiUtility.EncodeVersion("{3}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(deviceType))}: " +
                    $"{AnsiUtility.EncodeEnum("{4}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(requestParameters))}: " +
                    $"{AnsiUtility.EncodeRequestParameters("{5}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                    $"{AnsiUtility.EncodeErrorMessage("{6}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                    $"{AnsiUtility.EncodeErrorMessage("{7}")}",
                args: new object[]
                {
                        requestName,
                        apiVersion,
                        targetedApiVersion,
                        consumerVersion,
                        deviceType.GetDisplayName(),
                        requestParameters,
                        exceptionMessage,
                        innerExceptionMessage
                }
            );

            return _slackService
                .LogRequestError(
                    requestName: requestName,
                    apiVersion: apiVersion,
                    targetedApiVersion: targetedApiVersion,
                    consumerVersion: consumerVersion,
                    deviceType: deviceType,
                    requestParameters: requestParameters,
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
