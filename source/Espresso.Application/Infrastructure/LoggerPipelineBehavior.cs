using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
using MediatR;

namespace Espresso.Application.Infrastructure
{
    public class LoggerPipelineBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        #region Fields
        private readonly Stopwatch _stopWatch;
        private readonly ILoggerService _loggerService;
        #endregion

        #region Constructors
        public LoggerPipelineBehavior(
            ILoggerService loggerService
            )
        {
            _stopWatch = new Stopwatch();
            _loggerService = loggerService;
        }
        #endregion

        #region Methods
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestBase = request switch
            {
                Request<TResponse> baseType => baseType,
                _ => null
            };

            try
            {
                _stopWatch.Start();
                var response = await next().ConfigureAwait(false);
                _stopWatch.Stop();
                _loggerService.LogRequest(
                    requestId: (int)(requestBase?.EventIdEnum ?? Event.Undefined),
                    requestName: typeof(TRequest).Name,
                    webApiVersion: requestBase?.CurrentEspressoWebApiVersion ?? "",
                    targetedWebApiVersion: requestBase?.EspressoWebApiVersion ?? "",
                    consumerVersion: requestBase?.Version ?? "",
                    deviceType: requestBase?.DeviceType ?? DeviceType.Undefined,
                    requestParameters: request?.ToString() ?? "",
                    duration: _stopWatch.Elapsed,
                    responseData: response?.ToString() ?? "",
                    cancellationToken: cancellationToken
                );
                return response;
            }
            catch (Exception exception)
            {
                await _loggerService.LogRequestError(
                    requestId: (int)(requestBase?.EventIdEnum ?? Event.Undefined),
                    requestName: typeof(TRequest).Name,
                    webApiVersion: requestBase?.CurrentEspressoWebApiVersion ?? "",
                    targetedWebApiVersion: requestBase?.EspressoWebApiVersion ?? "",
                    consumerVersion: requestBase?.Version ?? "",
                    deviceType: requestBase?.DeviceType ?? DeviceType.Undefined,
                    requestParameters: request?.ToString() ?? "",
                    exception: exception,
                    cancellationToken: default
                ).ConfigureAwait(continueOnCapturedContext: false);

                throw;
            }
        }
        #endregion
    }
}
