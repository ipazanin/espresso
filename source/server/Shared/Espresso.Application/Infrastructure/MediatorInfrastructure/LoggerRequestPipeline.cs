// LoggerRequestPipeline.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Diagnostics;
using Espresso.Application.Models;
using Espresso.Common.Enums;
using Espresso.Domain.IServices;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure;

/// <summary>
/// <see cref="Mediator"/> pipeline for request logging.
/// </summary>
/// <typeparam name="TRequest">Mediator request.</typeparam>
/// <typeparam name="TResponse">Mediator response.</typeparam>
public class LoggerRequestPipeline<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _stopWatch;
    private readonly IMemoryCache _memoryCache;
    private readonly ILoggerService<LoggerRequestPipeline<TRequest, TResponse>> _loggerService;
    private readonly ApplicationInformation _applicationInformation;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggerRequestPipeline{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="memoryCache">Memory cache.</param>
    /// <param name="loggerService">Logger service.</param>
    /// <param name="applicationInformation">Application information.</param>
    public LoggerRequestPipeline(
        IMemoryCache memoryCache,
        ILoggerService<LoggerRequestPipeline<TRequest, TResponse>> loggerService,
        ApplicationInformation applicationInformation)
    {
        _stopWatch = new Stopwatch();
        _memoryCache = memoryCache;
        _loggerService = loggerService;
        _applicationInformation = applicationInformation;
    }

    /// <inheritdoc/>
    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var requestBase = request switch
        {
            Request<TResponse> baseType => baseType,
            _ => null
        };
        var requestName = typeof(TRequest).Name;
        var targetedApiVersion = requestBase?.TargetedApiVersion ?? string.Empty;
        var consumerVersion = requestBase?.ConsumerVersion ?? string.Empty;
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
        string requestName)
    {
        var performanceMeasurementKey = $"{requestName}PerformanceKey";
        var (total, count) = _memoryCache.GetOrCreate(
            key: performanceMeasurementKey,
            factory: _ =>
            {
                var total = default(TimeSpan);
                const int Count = 0;
                return (total, Count);
            });
        total += duration;
        count++;

        _memoryCache.Set(key: performanceMeasurementKey, value: (total, count));

        return total / count;
    }
}
