using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Application.Models;
using Espresso.Domain.IServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    public class ExceptionRequestPipeline<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        #region Fields
        public ILoggerService<ExceptionRequestPipeline<TRequest, TResponse>> _loggerService;
        private readonly ISlackService _slackService;
        private readonly ApplicationInformation _applicationInformation;
        #endregion

        #region Constructors
        public ExceptionRequestPipeline(
            ILoggerService<ExceptionRequestPipeline<TRequest, TResponse>> loggerService,
            ISlackService slackService,
            ApplicationInformation applicationInformation
        )
        {
            _loggerService = loggerService;
            _slackService = slackService;
            _applicationInformation = applicationInformation;
        }

        #endregion

        #region Methods
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                var requestName = typeof(TRequest).Name;
                var arguments = new List<(string argumentName, object argumentValue)>
                {
                    ("Version", _applicationInformation.Version),
                    ("App Environment", _applicationInformation.AppEnvironment),
                    ("Request Parameters", request.ToString() ?? ""),
                };

                _loggerService.Log(
                    eventName: requestName,
                    exception: exception,
                    logLevel: LogLevel.Error,
                    namedArguments: arguments
                );

                await _slackService
                    .LogError(
                        eventName: requestName,
                        message: $"Error while handling {requestName}",
                        exception: exception,
                        cancellationToken: cancellationToken
                    );

                throw;
            }

        }
    }
    #endregion
}