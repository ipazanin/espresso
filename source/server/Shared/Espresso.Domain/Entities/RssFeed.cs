// RssFeed.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

#pragma warning disable RCS1170

namespace Espresso.Domain.Entities;

public class RssFeed
{
    public const int UrlMaxLength = 300;

    public const int AmpConfigurationTemplateUrlMaxLength = 300;

    /// <summary>
    /// Initializes a new instance of the <see cref="RssFeed"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="url"></param>
    /// <param name="newsPortalId"></param>
    /// <param name="categoryId"></param>
    /// <param name="requestType"></param>
    public RssFeed(
        int id,
        string url,
        int newsPortalId,
        int categoryId,
        RequestType requestType)
    {
        Id = id;
        Url = url;
        NewsPortalId = newsPortalId;
        CategoryId = categoryId;
        RequestType = requestType;
        CategoryParseConfiguration = null!;
        ImageUrlParseConfiguration = null!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RssFeed"/> class.
    /// ORM Constructor.
    /// </summary>
    private RssFeed()
    {
        Url = null!;
        CategoryParseConfiguration = null!;
        ImageUrlParseConfiguration = null!;
    }

    /// <summary>
    /// Gets <see cref="RssFeed"/> id.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets <see cref="RssFeed"/> url.
    /// </summary>
    public string Url { get; private set; }

    public RequestType RequestType { get; private set; }

    public AmpConfiguration? AmpConfiguration { get; private set; }

    public CategoryParseConfiguration CategoryParseConfiguration { get; private set; }

    public ImageUrlParseConfiguration ImageUrlParseConfiguration { get; private set; }

    public SkipParseConfiguration? SkipParseConfiguration { get; private set; }

    public ICollection<RssFeedContentModifier> RssFeedContentModifiers { get; private set; } = new List<RssFeedContentModifier>();

    public ICollection<RssFeedCategory> RssFeedCategories { get; private set; } = new List<RssFeedCategory>();

    public int NewsPortalId { get; private set; }

    public NewsPortal? NewsPortal { get; private set; }

    public int CategoryId { get; private set; }

    public Category? Category { get; private set; }

    public ICollection<Article> Articles { get; private set; } = new List<Article>();

    public bool ShouldParse()
    {
        AmpConfiguration = new AmpConfiguration(false, string.Empty);

        return NewsPortal?.IsEnabled == true && SkipParseConfiguration?.ShouldParse() != false;
    }

    public string ModifyContent(string feedContent)
    {
        var orderedRssFeedContentModifiers = RssFeedContentModifiers
            .OrderBy(rssFeedContentModifier => rssFeedContentModifier.OrderIndex);

        foreach (var rssFeedContentModifier in orderedRssFeedContentModifiers)
        {
            feedContent = feedContent.Replace(rssFeedContentModifier.SourceValue, rssFeedContentModifier.ReplacementValue);
        }

        return feedContent;
    }
}
