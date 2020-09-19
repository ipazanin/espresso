using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
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
    public class RssFeedService : IRssFeedService
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        private readonly ISlackService _slackService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<RssFeedService> _logger;
        #endregion

        #region Constructors
        public RssFeedService(
            IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache,
            ISlackService slackService,
            ILoggerFactory loggerFactory
        )
        {
            _memoryCache = memoryCache;
            _slackService = slackService;
            _httpClient = httpClientFactory.CreateClient();
            _logger = loggerFactory.CreateLogger<RssFeedService>();
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
            string? feedContent;

            if (
                rssFeed.NewsPortalId == (int)NewsPortalId.SportskeNovosti ||
                rssFeed.NewsPortalId == (int)NewsPortalId.JutarnjiList ||
                rssFeed.NewsPortalId == (int)NewsPortalId.NarodHr ||
                rssFeed.NewsPortalId == (int)NewsPortalId.StoPosto ||
                rssFeed.NewsPortalId == (int)NewsPortalId.PoslovniPuls ||
                rssFeed.NewsPortalId == (int)NewsPortalId.SlobodnaDalmacija ||
                rssFeed.NewsPortalId == (int)NewsPortalId.N1 ||
                rssFeed.NewsPortalId == (int)NewsPortalId.LikaExpress
            )
            {
                feedContent = await LoadCompressedFeedContent(rssFeed, cancellationToken);
            }
            else
            {
                feedContent = await LoadFeedContent(rssFeed, cancellationToken);
            }

            var modifiedFeedContent = feedContent
                .Replace("version=\"2.0\" version=\"2.0\"", "version=\"2.0\"")
                .Replace("<rss version=\"2.0\">", "<rss xmlns:a10=\"http://www.w3.org/2005/Atom\" version=\"2.0\">")
                .Trim();

            if (
                rssFeed.Id == (int)RssFeedId.Index_Auto ||
                rssFeed.Id == (int)RssFeedId.Index_Magazin ||
                rssFeed.Id == (int)RssFeedId.Index_Rogue ||
                rssFeed.Id == (int)RssFeedId.Index_Sport ||
                rssFeed.Id == (int)RssFeedId.Index_Vijesti ||
                rssFeed.Id == (int)RssFeedId.IndexHrZagreb
            )
            {
                modifiedFeedContent = modifiedFeedContent.Replace("<content>", "<a10:content>");
                modifiedFeedContent = modifiedFeedContent.Replace("</content>", "</a10:content>");
            }


            var reader = XmlReader.Create(
                input: new StringReader(modifiedFeedContent)
            );
            var feed = SyndicationFeed.Load(reader);

            return feed;
        }

        private async Task<string> LoadCompressedFeedContent(RssFeed rssFeed, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, rssFeed.Url);
            // _ = request.Headers.TryAddWithoutValidation("accept", "text/html,application/xhtml+xml,application/xml");
            _ = request.Headers.TryAddWithoutValidation("accept", "*/*");
            _ = request.Headers.TryAddWithoutValidation("accept-encoding", "gzip, deflate, br");
            // _ = request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            _ = request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Mobile Safari/537.36");
            // _ = request.Headers.TryAddWithoutValidation("accept-charset", "ISO-8859-1");

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
