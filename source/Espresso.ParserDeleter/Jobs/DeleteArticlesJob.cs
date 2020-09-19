﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Articles.Commands.DeleteOldArticles;
using Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Initialization;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using Espresso.ParserDeleter.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Espresso.Jobs
{
    public class DeleteArticlesJob : BackgroundService
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IParserDeleterConfiguration _configuration;
        private readonly ISlackService _slackService;
        private readonly ILogger<ParseArticlesJob> _logger;
        #endregion

        #region Constructors
        public DeleteArticlesJob(
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration configuration,
            ISlackService slackService,
            ILoggerFactory loggerFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _slackService = slackService;
            _logger = loggerFactory.CreateLogger<ParseArticlesJob>();
        }
        #endregion

        #region Methods
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await Task.Delay(
                delay: _configuration.DateTimeConfiguration.WaitDurationBeforeStartup.Add(TimeSpan.FromSeconds(45)),
                cancellationToken: stoppingToken
            );


            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var cancellationToken = GetCancellationToken();

                    await mediator.Send(
                        request: new DeleteOldArticlesCommand(
                            maxAgeOfOldArticles: _configuration.DateTimeConfiguration.MaxAgeOfArticles,
                            currentEspressoWebApiVersion: AppConfiguration.RssFeedParserVersion,
                            targetedEspressoWebApiVersion: AppConfiguration.RssFeedParserMajorMinorVersion,
                            consumerVersion: AppConfiguration.RssFeedParserVersion,
                            deviceType: DeviceType.RssFeedParser,
                            appEnvironment: _configuration.AppConfiguration.AppEnvironment
                        ),
                        cancellationToken: cancellationToken
                    );

                    await Task.Delay(_configuration.DateTimeConfiguration.WaitDurationBetweenDeleteArticlesJobs, stoppingToken);
                }
                catch (Exception exception)
                {
                    var eventName = Event.DeleteArticlesJob.GetDisplayName();
                    var eventId = (int)Event.DeleteArticlesJob;
                    var version = AppConfiguration.Version;
                    var exceptionMessage = exception.Message;
                    var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;

                    _logger.LogError(
                        eventId: new EventId(
                            id: eventId,
                            name: eventName
                        ),
                        exception: exception,
                        message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                            $"{AnsiUtility.EncodeParameterName(nameof(version))}: " +
                            $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                            $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                            $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                            $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                            $"{AnsiUtility.EncodeErrorMessage("{3}")}",
                        args: new object[]
                        {
                            eventName,
                            version,
                            exceptionMessage,
                            innerExceptionMessage,
                        }
                    );

                    await _slackService.LogError(
                            eventName: eventName,
                            version: AppConfiguration.Version,
                            message: exception.Message,
                            exception: exception,
                            appEnvironment: _configuration.AppConfiguration.AppEnvironment,
                            cancellationToken: default
                    );

                    await Task.Delay(_configuration.DateTimeConfiguration.WaitDurationAfterErrors, stoppingToken);
                }
            }
        }

        private CancellationToken GetCancellationToken()
        {
            var cancellationTokeSource = new CancellationTokenSource(
                delay: _configuration.DateTimeConfiguration.CancellationTokenExpirationDuration
            );
            return cancellationTokeSource.Token;
        }
        #endregion
    }
}