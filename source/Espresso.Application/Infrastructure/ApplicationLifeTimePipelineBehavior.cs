using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.Infrastructure
{
    public class ApplicationLifeTimePipelineBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        #region Constants
        private static TimeSpan DeadLockThreshold => TimeSpan.FromMinutes(10);
        #endregion

        #region Fields
        private readonly ILoggerService _loggerService;
        private readonly IMemoryCache _memoryCache;
        //private readonly ILogger<TRequest> _logger;
        #endregion

        #region Properties
        public int RequestId { get; set; }
        public string RequestName { get; set; } = "";
        public string WebApiVersion { get; set; } = "";
        public string TargetedWebApiVersion { get; set; } = "";
        public string ConsumerVersion { get; set; } = "";
        public DeviceType DeviceType { get; set; }
        public string RequestParameters { get; set; } = "";
        #endregion

        #region Constructors
        public ApplicationLifeTimePipelineBehavior(
            IMemoryCache memoryCache,
            ILoggerService loggerService
        )
        {
            _loggerService = loggerService;
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
            WebApiVersion = requestBase?.CurrentEspressoWebApiVersion ?? "";
            TargetedWebApiVersion = requestBase?.TargetedEspressoWebApiVersion ?? "";
            ConsumerVersion = requestBase?.ConsumerVersion ?? "";
            DeviceType = requestBase?.DeviceType ?? DeviceType.Undefined;

            using var timer = new System.Timers.Timer(DeadLockThreshold.TotalMilliseconds);
            timer.Elapsed += TerminateProcess;
            timer.Start();
            try
            {
                var response = await next()
                    .ConfigureAwait(false);

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
            var memoryCacheLogMessage = _memoryCache.Get<string>(MemoryCacheConstants.DeadLockLogKey);
            _loggerService.LogRequestError(
                requestId: RequestId,
                requestName: RequestName,
                webApiVersion: WebApiVersion,
                targetedWebApiVersion: TargetedWebApiVersion,
                consumerVersion: ConsumerVersion,
                deviceType: DeviceType,
                requestParameters: RequestParameters,
                exception: new Exception($"Deadlocked at {memoryCacheLogMessage}!"),
                cancellationToken: default
            )
                .GetAwaiter()
                .GetResult();
            Environment.Exit(-1);
        }
        #endregion
    }
}
