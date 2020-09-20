using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Espresso.Application.Extensions;
using Espresso.Application.IService;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Espresso.Application.Services
{
    public class RssFeedLoadService : IRssFeedLoadService
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly ISlackService _slackService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<RssFeedLoadService> _logger;
        #endregion

        #region Constructors
        public RssFeedLoadService(
            IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache,
            ISlackService slackService,
            ILoggerFactory loggerFactory
        )
        {
            _memoryCache = memoryCache;
            _slackService = slackService;
            _httpClient = httpClientFactory.CreateClient();
            _logger = loggerFactory.CreateLogger<RssFeedLoadService>();
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<(SyndicationFeed SyndicationFeed, RssFeed rssFeed)>> ParseRssFeeds(
            IEnumerable<RssFeed> rssFeeds,
            AppEnvironment appEnvironment,
            string currentApiVersion,
            CancellationToken cancellationToken
        )
        {
            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                value: $"Started {nameof(ParseRssFeeds)}"
            );

            var parsedArticles = new ConcurrentQueue<(SyndicationFeed SyndicationFeed, RssFeed rssFeed)>();

            var getRssFeedRequestTasks = new List<Task>();

            foreach (var rssFeed in rssFeeds)
            {
                if (!rssFeed.ShouldParse())
                {
                    continue;
                }

                getRssFeedRequestTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var feed = await LoadFeed(rssFeed, cancellationToken);

                        if (rssFeed.NewsPortalId == (int)NewsPortalId.Nacional)
                        {
                        }

                        parsedArticles.Enqueue((feed, rssFeed));
                    }
                    catch (Exception exception)
                    {
                        var rssFeedUrl = rssFeed.Url;
                        var exceptionMessage = exception.Message;
                        var eventName = Event.RssFeedLoading.GetDisplayName();
                        var eventId = (int)Event.RssFeedLoading;
                        var message = $"RssFeedUrl: {rssFeedUrl}";
                        var innerExceptionMessage = exception.InnerException?.Message ?? "";

                        _logger.LogWarning(
                            eventId: new EventId(id: eventId, name: eventName),
                            message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                                $"{AnsiUtility.EncodeParameterName(nameof(message))}: " +
                                $"{AnsiUtility.EncodeRequestParameters("{1}")}\n\t" +
                                $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                                $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                                $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                                $"{AnsiUtility.EncodeErrorMessage("{3}")}",
                            args: new object[]
                            {
                                eventName,
                                message,
                                exceptionMessage,
                                innerExceptionMessage,
                            }
                        );

                        await _slackService.LogWarning(
                            eventName: eventName,
                            version: currentApiVersion,
                            message: message,
                            exception: exception,
                            appEnvironment: appEnvironment,
                            cancellationToken: cancellationToken
                        );
                    }
                }, cancellationToken));
            }

            await Task.WhenAll(getRssFeedRequestTasks);

            _ = _memoryCache.Set(
                key: MemoryCacheConstants.DeadLockLogKey,
                            value: $"Ended {nameof(ParseRssFeeds)}"
            );

            return parsedArticles;
        }

        private async Task<SyndicationFeed> LoadFeed(RssFeed rssFeed, CancellationToken cancellationToken)
        {
            var feedContent = rssFeed.RequestType switch
            {
                RequestType.Browser => await LoadCompressedFeedContent(rssFeed, cancellationToken),
                RequestType.Normal => await LoadFeedContent(rssFeed, cancellationToken),
                _ => await LoadFeedContent(rssFeed, cancellationToken)
            };

            var modifiedFeedContent = rssFeed.ModifyContent(feedContent);


            var reader = XmlReader.Create(
                input: new StringReader(modifiedFeedContent)
            );
            var feed = SyndicationFeed.Load(reader);

            return feed;
        }

        private async Task<string> LoadCompressedFeedContent(RssFeed rssFeed, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, rssFeed.Url);
            request.AddBrowserHeadersToHttpRequestMessage();

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            _ = response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
            using var streamReader = new StreamReader(decompressedStream);
            var feedContent = await streamReader.ReadToEndAsync();

            return feedContent;
        }

        private async Task<string> LoadFeedContent(RssFeed rssFeed, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, rssFeed.Url);

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            _ = response.EnsureSuccessStatusCode();

            var feedContent = await response.Content.ReadAsStringAsync();

            return feedContent;
        }
        #endregion
    }
}
