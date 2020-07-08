using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Espresso.Common.Enums;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Articles.Commands.DeleteOldArticles;
using Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds;
using Espresso.Application.DataTransferObjects;
using Espresso.Application.Initialization;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using MediatR;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using Espresso.Workers.ParserDeleter.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Espresso.Workers.ParserDeleter
{
    public class ParserDeleter : BackgroundService
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IParserDeleterConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerService _loggerService;
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public ParserDeleter(
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILoggerService loggerService,
            IMemoryCache memoryCache
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _loggerService = loggerService;
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

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

            while (numberOfTries-- > 0)
            {
                try
                {
                    var content = await CreateWebServerContent(
                        parseRssFeedsResponse: parseRssFeedsResponse,
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(continueOnCapturedContext: false);

                    using var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Add(HttpHeaderConstants.HeaderName, apiKey);
                    client.DefaultRequestHeaders.Add(HttpHeaderConstants.EspressoApiHeaderName, _configuration.RssFeedParserMajorMinorVersion);
                    client.DefaultRequestHeaders.Add(HttpHeaderConstants.VersionHeaderName, _configuration.RssFeedParserVersion);
                    client.DefaultRequestHeaders.Add(HttpHeaderConstants.DeviceTypeHeaderName, ((int)DeviceType.RssFeedParser).ToString());
                    client.Timeout = TimeSpan.FromMinutes(3);

                    var response = await client
                        .PostAsync(requestUri: $"{_configuration.ServerUrl}/api/notifications", content: content)
                        .ConfigureAwait(false);
                    var resMessage = await response.Content.ReadAsStringAsync();
                    var status = response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        break;
                    }
                    await Task.Delay(_configuration.WaitDurationAfterWebServerRequestError).ConfigureAwait(false);
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

        private async Task<HttpContent> CreateWebServerContent(
            ParseRssFeedsCommandResponse parseRssFeedsResponse,
            CancellationToken cancellationToken
        )
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, new ArticlesRequestObjectDto
            {
                CreatedArticles = parseRssFeedsResponse.CreatedArticles,
                UpdatedArticles = parseRssFeedsResponse.UpdatedArticles
            }, cancellationToken: cancellationToken).ConfigureAwait(false);

            stream.Position = 0;
            using var reader = new StreamReader(stream);
            var jsonString = await reader.ReadToEndAsync().ConfigureAwait(false);

            var content = new StringContent(jsonString, Encoding.UTF8, MimeTypeConstants.Json);
            return content;
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
