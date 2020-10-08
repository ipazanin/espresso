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
using Espresso.Application.IServices;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.NewsPortalEnums;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.Extensions;
using Espresso.Domain.Records;
using Espresso.ParserDeleter.Application.IServices;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.Application.Services
{
    public class LoadRssFeedsService : ILoadRssFeedsService
    {
        #region Fields
        private readonly ISlackService _slackService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoadRssFeedsService> _logger;
        #endregion

        #region Constructors
        public LoadRssFeedsService(
            IHttpClientFactory httpClientFactory,
            ISlackService slackService,
            ILoggerFactory loggerFactory
        )
        {
            _slackService = slackService;
            _logger = loggerFactory.CreateLogger<LoadRssFeedsService>();
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<RssFeedItem>> ParseRssFeeds(
            IEnumerable<RssFeed> rssFeeds,
            AppEnvironment appEnvironment,
            string currentApiVersion,
            CancellationToken cancellationToken
        )
        {
            var parsedArticles = new ConcurrentQueue<RssFeedItem>();

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

                        foreach (var syndicationItem in feed.Items)
                        {
                            if (syndicationItem is null)
                            {
                                continue;
                            }

                            var rssFeedItem = new RssFeedItem
                            {
                                RssFeed = rssFeed,
                                Id = syndicationItem.Id,
                                Links = syndicationItem.Links?.Select(syndicationLink => syndicationLink.Uri),
                                Title = syndicationItem.Title?.Text,
                                Summary = syndicationItem.Summary?.Text,
                                Content = (syndicationItem.Content is TextSyndicationContent ?
                                    syndicationItem.Content as TextSyndicationContent
                                    : null)
                                    ?.Text,
                                PublishDateTime = syndicationItem.PublishDate.DateTime,
                                ElementExtensions = syndicationItem
                                    .ElementExtensions
                                    ?.Select(elementExtension => elementExtension?.GetObject<string?>()),
                            };

                            parsedArticles.Enqueue(rssFeedItem);
                        }

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

            using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
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

            var feedContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return feedContent;
        }
        #endregion
    }
}
