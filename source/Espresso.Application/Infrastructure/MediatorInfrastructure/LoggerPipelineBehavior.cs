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
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public class LoggerPipelineBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        #region Fields
        private readonly Stopwatch _stopWatch;
        private readonly ISlackService _slackService;
        private readonly ILogger<LoggerPipelineBehavior<TRequest, TResponse>> _logger;
        #endregion

        #region Constructors
        public LoggerPipelineBehavior(
            ISlackService slackService,
            ILoggerFactory loggerFactory
        )
        {
            _stopWatch = new Stopwatch();
            _slackService = slackService;
            _logger = loggerFactory.CreateLogger<LoggerPipelineBehavior<TRequest, TResponse>>();
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
                        $"{AnsiUtility.EncodeDuration("{6}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(responseData))}: " +
                        $"{AnsiUtility.EncodeResponse("{7}")}",
                    args: new object[]
                    {
                        requestName,
                        apiVersion,
                        targetedApiVersion,
                        consumerVersion,
                        deviceType.GetDisplayName(),
                        requestParameters,
                        duration,
                        responseData
                    }
                );
                return response;
            }
            catch (Exception exception)
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

                await _slackService
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

                throw;
            }
        }
        #endregion
    }
}
