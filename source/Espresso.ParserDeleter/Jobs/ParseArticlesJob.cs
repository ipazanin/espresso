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
    public class ParseArticlesJob : BackgroundService
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IParserDeleterConfiguration _configuration;
        private readonly IHttpService _httpService;
        private readonly ISlackService _slackService;
        private readonly ILogger<ParseArticlesJob> _logger;
        #endregion

        #region Constructors
        public ParseArticlesJob(
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration configuration,
            IHttpService httpService,
            ISlackService slackService,
            ILoggerFactory loggerFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _httpService = httpService;
            _slackService = slackService;
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
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var cancellationToken = GetCancellationToken();

                    var parseRssFeedsCommandResponse = await mediator.Send(
                        request: new ParseRssFeedsCommand(
                            maxAgeOfArticle: _configuration.DateTimeConfiguration.MaxAgeOfArticles,
                            currentEspressoWebApiVersion: AppConfiguration.RssFeedParserVersion,
                            targetedEspressoWebApiVersion: AppConfiguration.RssFeedParserMajorMinorVersion,
                            consumerVersion: AppConfiguration.RssFeedParserVersion,
                            deviceType: DeviceType.RssFeedParser,
                            appEnvironment: _configuration.AppConfiguration.AppEnvironment
                        ),
                        cancellationToken: cancellationToken
                    );

                    await CallWebServer(parseRssFeedsCommandResponse, cancellationToken);

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

        private async Task CallWebServer(
            ParseRssFeedsCommandResponse parseRssFeedsResponse,
            CancellationToken cancellationToken
        )
        {
            if (
                !parseRssFeedsResponse.CreatedArticles.Any() &&
                !parseRssFeedsResponse.UpdatedArticles.Any()
            )
            {
                return;
            }

            var numberOfTries = 5;
            var apiKey = _configuration.ApiKeysConfiguration.ParserApiKey;
            var httpHeaders = new List<(string headerKey, string headerValue)>
            {
                (headerKey: HttpHeaderConstants.ApiKeyHeaderName, headerValue: apiKey),
                (headerKey: HttpHeaderConstants.EspressoApiHeaderName, headerValue: AppConfiguration.RssFeedParserMajorMinorVersion),
                (headerKey: HttpHeaderConstants.VersionHeaderName, headerValue: AppConfiguration.RssFeedParserVersion),
                (headerKey: HttpHeaderConstants.DeviceTypeHeaderName, headerValue: ((int)DeviceType.RssFeedParser).ToString()),
            };

            while (numberOfTries-- > 0)
            {
                try
                {
                    await _httpService.PostJsonAsync(
                        url: $"{_configuration.AppConfiguration.ServerUrl}/api/notifications/articles",
                        data: new ArticlesRequestObjectDto
                        {
                            CreatedArticles = parseRssFeedsResponse.CreatedArticles,
                            UpdatedArticles = parseRssFeedsResponse.UpdatedArticles
                        },
                        httpHeaders: httpHeaders,
                        httpClientTimeout: TimeSpan.FromSeconds(30),
                        cancellationToken: cancellationToken
                    );

                    return;
                }
                catch (Exception exception)
                {
                    var eventName = Event.ParserDeleterNewArticlesRequest.GetDisplayName();
                    var eventId = (int)Event.ParserDeleterNewArticlesRequest;
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
                    await Task.Delay(_configuration.DateTimeConfiguration.WaitDurationAfterWebServerRequestError, cancellationToken);
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