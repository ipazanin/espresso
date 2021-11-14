// LoadRssFeedsService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.Extensions;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Dashboard.Application.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.IServices;
using Espresso.Domain.Records;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace Espresso.Dashboard.Application.Services
{
    public class LoadRssFeedsService : ILoadRssFeedsService
    {
        private readonly ILoggerService<LoadRssFeedsService> _loggerService;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadRssFeedsService"/> class.
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="loggerService"></param>
        public LoadRssFeedsService(
            IHttpClientFactory httpClientFactory,
            ILoggerService<LoadRssFeedsService> loggerService)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientConstants.LoadRssFeedsHttpClientName);
            _loggerService = loggerService;
        }

        public async Task<Channel<RssFeedItem>> ParseRssFeeds(
            IEnumerable<RssFeed> rssFeeds,
            CancellationToken cancellationToken)
        {
            var rssFeedItemsChannel = Channel.CreateUnbounded<RssFeedItem>();
            var writer = rssFeedItemsChannel.Writer;

            var getRssFeedRequestTasks = new List<Task>();

            foreach (var rssFeed in rssFeeds)
            {
                if (!rssFeed.ShouldParse())
                {
                    continue;
                }

                var task = Task.Run(async () => await ParseRssFeed(rssFeed, writer, cancellationToken), cancellationToken);
                getRssFeedRequestTasks.Add(task);
            }

            await Task.WhenAll(getRssFeedRequestTasks);

            writer.Complete();

            return rssFeedItemsChannel;
        }

        private async Task ParseRssFeed(RssFeed rssFeed, ChannelWriter<RssFeedItem> writer, CancellationToken cancellationToken)
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
                        Content = (
                                syndicationItem.Content is TextSyndicationContent ?
                                syndicationItem.Content as TextSyndicationContent
                                : null)
                            ?.Text,
                        PublishDateTime = syndicationItem.PublishDate.DateTime,
                        ElementExtensions = syndicationItem
                            .ElementExtensions
                            ?.Select(elementExtension => elementExtension?.GetObject<string?>()),
                    };

                    _ = writer.TryWrite(rssFeedItem);
                }
            }
            catch (Exception exception)
            {
                var rssFeedUrl = rssFeed.Url;
                var eventName = Event.RssFeedLoading.GetDisplayName();
                var arguments = new (string, object)[]
                {
                    (nameof(rssFeedUrl), rssFeedUrl),
                    ("IsEnabled", rssFeed.NewsPortal?.IsEnabled.ToString() ?? "Empty"),
                };

                _loggerService.Log(eventName, exception, LogLevel.Error, arguments);
            }
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
                input: new StringReader(modifiedFeedContent));
            var feed = SyndicationFeed.Load(reader);

            return feed;
        }

        private async Task<string> LoadCompressedFeedContent(RssFeed rssFeed, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, rssFeed.Url);
            request.AddBrowserHeadersToHttpRequestMessage();

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

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

            response.EnsureSuccessStatusCode();

            var feedContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return feedContent;
        }
    }
}
