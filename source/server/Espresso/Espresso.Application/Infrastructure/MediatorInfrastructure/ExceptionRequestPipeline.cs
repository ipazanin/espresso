// ExceptionRequestPipeline.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Models;
using Espresso.Application.Services.Contracts;
using Espresso.Domain.IServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    /// <summary>
    /// <see cref="Mediator"/> pipeline to catch and handle uncaught exceptions.
    /// </summary>
    /// <typeparam name="TRequest">Mediator request.</typeparam>
    /// <typeparam name="TResponse">Mediator response.</typeparam>
    public class ExceptionRequestPipeline<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILoggerService<ExceptionRequestPipeline<TRequest, TResponse>> _loggerService;
        private readonly ISlackService _slackService;
        private readonly ApplicationInformation _applicationInformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionRequestPipeline{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="loggerService">Logger service.</param>
        /// <param name="slackService">Slack service.</param>
        /// <param name="applicationInformation">Application information.</param>
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

        /// <inheritdoc/>
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
                    ("Request Parameters", request.ToString() ?? string.Empty),
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
}
