using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Articles.Commands.DeleteOldArticles;
using Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Initialization;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using Espresso.Workers.ParserDeleter.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Espresso.Workers.ParserDeleter
{
    public class ParserDeleter : BackgroundService
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IParserDeleterConfiguration _configuration;
        private readonly ILoggerService _loggerService;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpService _httpService;
        #endregion

        #region Constructors
        public ParserDeleter(
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration configuration,
            ILoggerService loggerService,
            IMemoryCache memoryCache,
            IHttpService httpService
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _loggerService = loggerService;
            _memoryCache = memoryCache;
            _httpService = httpService;
        }
        #endregion

        #region Methods
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await Task.Delay(TimeSpan.FromSeconds(40));

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
                            currentEspressoWebApiVersion: _configuration.RssFeedParserVersion,
                            espressoWebApiVersion: _configuration.RssFeedParserMajorMinorVersion,
                            version: _configuration.RssFeedParserVersion,
                            deviceType: DeviceType.RssFeedParser
                        ),
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);

                    await CallWebServer(parseRssFeedsCommandResponse, cancellationToken).ConfigureAwait(false);

                    await mediator.Send(
                        request: new DeleteOldArticlesCommand(
                            maxAgeOfOldArticles: _configuration.MaxAgeOfOldArticles,
                            currentEspressoWebApiVersion: _configuration.RssFeedParserVersion,
                            espressoWebApiVersion: _configuration.RssFeedParserMajorMinorVersion,
                            version: _configuration.RssFeedParserVersion,
                            deviceType: DeviceType.RssFeedParser
                        ),
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);

                    await Task.Delay(_configuration.WaitDurationBetweenCommands).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    await _loggerService.LogError(
                        eventId: (int)Event.ParserDeleterWebJob,
                        eventName: Event.ParserDeleterWebJob.GetDisplayName(),
                        version: _configuration.RssFeedParserVersion,
                        message: exception.Message,
                        exception: exception,
                        cancellationToken: stoppingToken
                    );
                    await Task.Delay(_configuration.WaitDurationAfterErrors).ConfigureAwait(false);
                }
            }
        }

        private async Task InitializeParser()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var memoryCacheInit = scope.ServiceProvider.GetRequiredService<IMemoryCacheInit>();
            await memoryCacheInit.InitParserDeleter();
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
            var apiKey = _memoryCache.Get<IEnumerable<string>>(key: MemoryCacheConstants.ApiKeysKey).First();
            var httpHeaders = new List<(string headerKey, string headerValue)>
            {
                (headerKey: HttpHeaderConstants.HeaderName, headerValue: apiKey),
                (headerKey: HttpHeaderConstants.EspressoApiHeaderName, headerValue: _configuration.RssFeedParserMajorMinorVersion),
                (headerKey: HttpHeaderConstants.VersionHeaderName, headerValue: _configuration.RssFeedParserVersion),
                (headerKey: HttpHeaderConstants.DeviceTypeHeaderName, headerValue: ((int)DeviceType.RssFeedParser).ToString()),
            };

            while (numberOfTries-- > 0)
            {
                try
                {
                    await _httpService.PostJsonAsync(
                        url: $"{_configuration.ServerUrl}/api/notifications",
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
                    await _loggerService.LogError(
                        eventId: (int)Event.ParserDeleterNewArticlesRequest,
                        eventName: Event.ParserDeleterNewArticlesRequest.GetDisplayName(),
                        version: _configuration.Version,
                        message: exception.Message,
                        exception: exception,
                        cancellationToken: cancellationToken
                    );
                    await Task.Delay(_configuration.WaitDurationAfterWebServerRequestError).ConfigureAwait(false);
                }
            }
        }


        private CancellationToken GetCancellationToken()
        {
            var cancellationTokeSource = new CancellationTokenSource(
                delay: _configuration.CancellationTokenExpirationDuration
            );
            return cancellationTokeSource.Token;
        }
        #endregion
    }
}
