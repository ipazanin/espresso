// LoadRssFeedsService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects;
using Espresso.Application.Extensions;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Dashboard.Application.Constants;
using Espresso.Dashboard.Application.IServices;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.IServices;
using Espresso.Domain.Records;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace Espresso.Dashboard.Application.Services;

public class LoadRssFeedsService : ILoadRssFeedsService
{
    private const int RssFeedChunkSize = 30;
    private const int RssFeedItemChannelCapacity = 200;

    private static readonly BoundedChannelOptions s_boundedChannelOptions = new(RssFeedItemChannelCapacity)
    {
        FullMode = BoundedChannelFullMode.Wait,
    };

    private readonly ILoggerService<LoadRssFeedsService> _loggerService;
    private readonly IParsingMessagesService _parseWarningsService;
    private readonly HttpClient _httpClient;
    private readonly Channel<RssFeedItem> _rssFeedItemsChannel = Channel.CreateBounded<RssFeedItem>(s_boundedChannelOptions);

    /// <summary>
    /// Initializes a new instance of the <see cref="LoadRssFeedsService"/> class.
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="loggerService"></param>
    /// <param name="parseWarningsService"></param>
    public LoadRssFeedsService(
        IHttpClientFactory httpClientFactory,
        ILoggerService<LoadRssFeedsService> loggerService,
        IParsingMessagesService parseWarningsService)
    {
        _httpClient = httpClientFactory.CreateClient(HttpClientConstants.LoadRssFeedsHttpClientName);
        _loggerService = loggerService;
        _parseWarningsService = parseWarningsService;
    }

    public ChannelReader<RssFeedItem> RssFeedItemsChannelReader => _rssFeedItemsChannel.Reader;

    public async Task ParseRssFeeds(
        IEnumerable<RssFeed> rssFeeds,
        CancellationToken cancellationToken)
    {
        var getRssFeedRequestTasks = new List<Task>();

        var chunkedRssFeeds = rssFeeds.Chunk(RssFeedChunkSize);

        foreach (var rssFeedChunk in chunkedRssFeeds)
        {
            foreach (var rssFeed in rssFeedChunk)
            {
                if (!rssFeed.ShouldParse())
                {
                    continue;
                }

                var task = Task.Run(async () => await ParseRssFeed(rssFeed, _rssFeedItemsChannel.Writer, cancellationToken), cancellationToken);
                getRssFeedRequestTasks.Add(task);
            }

            await Task.WhenAll(getRssFeedRequestTasks);
        }

        _rssFeedItemsChannel.Writer.Complete();
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
                        ?.Select(e => e.GetObject<XElement>())
                        ?? Enumerable.Empty<XElement>(),
                };

                await writer.WriteAsync(rssFeedItem, cancellationToken);
            }
        }
        catch (Exception exception)
        {
            var rssFeedUrl = rssFeed.Url;
            var eventName = Event.RssFeedLoading.GetDisplayName();
            var arguments = new (string, object)[]
            {
                    (nameof(rssFeedUrl), rssFeedUrl),
                    ("IsEnabled", rssFeed.NewsPortal!.IsEnabled.ToString()),
            };

            _loggerService.Log(eventName, exception, LogLevel.Error, arguments);

            var parsingErrorMessage = new ParsingErrorMessageDto(
                logLevel: LogLevel.Error,
                message: $"RssFeed loading exception: {exception.Message}",
                rssFeedId: rssFeed.Id);
            _parseWarningsService.PushMessage(parsingErrorMessage);
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
        var feedContent = await streamReader.ReadToEndAsync(cancellationToken);

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
