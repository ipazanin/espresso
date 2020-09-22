using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public class ApplicationLifeTimePipelineBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        #region Constants
        private static TimeSpan DeadLockThreshold => TimeSpan.FromMinutes(15);
        #endregion

        #region Fields
        private readonly ISlackService _slackService;
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Properties
        public int RequestId { get; set; }
        public string RequestName { get; set; } = "";
        public string WebApiVersion { get; set; } = "";
        public string TargetedWebApiVersion { get; set; } = "";
        public string ConsumerVersion { get; set; } = "";
        public DeviceType DeviceType { get; set; }
        public string RequestParameters { get; set; } = "";
        public AppEnvironment AppEnvironment { get; set; }
        #endregion

        #region Constructors
        public ApplicationLifeTimePipelineBehavior(
            IMemoryCache memoryCache,
            ISlackService slackService
        )
        {
            _slackService = slackService;
            _memoryCache = memoryCache;
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
                _ => null
            };

            RequestName = typeof(TRequest).Name;
            RequestId = (int)(requestBase?.EventIdEnum ?? Event.Undefined);
            RequestParameters = request?.ToString() ?? "";
            WebApiVersion = requestBase?.CurrentApiVersion ?? "";
            TargetedWebApiVersion = requestBase?.TargetedApiVersion ?? "";
            ConsumerVersion = requestBase?.ConsumerVersion ?? "";
            DeviceType = requestBase?.DeviceType ?? DeviceType.Undefined;
            AppEnvironment = requestBase?.AppEnvironment ?? AppEnvironment.Undefined;

            using var timer = new System.Timers.Timer(DeadLockThreshold.TotalMilliseconds);
            timer.Elapsed += TerminateProcess;
            timer.Start();
            try
            {
                var response = await next();

                timer.Stop();

                return response;
            }
            catch (Exception)
            {
                timer.Stop();
                throw;
            }
        }

        private void TerminateProcess(object source, ElapsedEventArgs e)
        {
            var memoryCacheLogMessage = _memoryCache
                .GetOrCreate(
                    key: MemoryCacheConstants.DeadLockLogKey,
                    factory: cacheEntry => ""
                );

            _slackService
                .LogRequestError(
                    requestName: RequestName,
                    apiVersion: WebApiVersion,
                    targetedApiVersion: TargetedWebApiVersion,
                    consumerVersion: ConsumerVersion,
                    deviceType: DeviceType,
                    requestParameters: RequestParameters,
                    exception: new Exception($"Deadlocked at {memoryCacheLogMessage}!"),
                    appEnvironment: AppEnvironment,
                    cancellationToken: default
                )
                .GetAwaiter()
                .GetResult();

            Environment.Exit(-1);
        }
        #endregion
    }
}
