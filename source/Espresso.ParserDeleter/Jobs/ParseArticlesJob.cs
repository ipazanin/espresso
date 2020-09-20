using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Initialization;
using Espresso.Application.IServices;
using Espresso.Application.Services;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using Espresso.ParserDeleter.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Espresso.Jobs
{
    public class ParseArticlesJob : BackgroundService
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IParserDeleterConfiguration _configuration;
        private readonly ILogger<ParseArticlesJob> _logger;
        #endregion

        #region Constructors
        public ParseArticlesJob(
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration configuration,
            ILoggerFactory loggerFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<ParseArticlesJob>();
        }
        #endregion

        #region Methods
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await Task.Delay(_configuration.DateTimeConfiguration.WaitDurationBeforeStartup, stoppingToken);

            await InitializeParser();

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var cancellationToken = GetCancellationToken();

                try
                {
                    var parseRssFeedsCommandResponse = await mediator.Send(
                        request: new ParseRssFeedsCommand(
                            maxAgeOfArticle: _configuration.DateTimeConfiguration.MaxAgeOfArticles,
                            parserApiKey: _configuration.ApiKeysConfiguration.ParserApiKey,
                            waitDurationAfterWebServerRequestError: _configuration.DateTimeConfiguration.WaitDurationAfterWebServerRequestError,
                            serverUrl: _configuration.AppConfiguration.ServerUrl,
                            currentEspressoWebApiVersion: AppConfiguration.RssFeedParserVersion,
                            targetedEspressoWebApiVersion: AppConfiguration.RssFeedParserMajorMinorVersion,
                            consumerVersion: AppConfiguration.RssFeedParserVersion,
                            deviceType: DeviceType.RssFeedParser,
                            appEnvironment: _configuration.AppConfiguration.AppEnvironment
                        ),
                        cancellationToken: cancellationToken
                    );

                    await Task.Delay(_configuration.DateTimeConfiguration.WaitDurationBetweenParseArticlesJobs, stoppingToken);
                }
                catch (Exception exception)
                {
                    var eventName = Event.ParseArticlesJob.GetDisplayName();
                    var eventId = (int)Event.ParseArticlesJob;
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

                    await slackService.LogError(
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

        private async Task InitializeParser()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var memoryCacheInit = scope.ServiceProvider.GetRequiredService<IApplicationInit>();
            await memoryCacheInit.InitParserDeleter();
        }
        #endregion
    }
}
