// RssFeedItem.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Common.Extensions;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails;

/// <summary>
/// <see cref="NewsPortal"/> details <see cref="RssFeed"/> item.
/// </summary>
public sealed class RssFeedItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RssFeedItem"/> class.
    /// </summary>
    private RssFeedItem()
    {
        Url = null!;
        Category = null!;
        RssFeedCategories = null!;
        CategoryParseStrategy = null!;
    }

    /// <summary>
    /// Gets <see cref="RssFeed"/> id.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets <see cref="RssFeed"/> url.
    /// </summary>
    public string Url { get; private set; }

    /// <summary>
    /// Gets <see cref="RssFeed"/> <see cref="Category.Name"/>.
    /// </summary>
    public string Category { get; private set; }

    /// <summary>
    /// Gets <see cref="Domain.Enums.RssFeedEnums.CategoryParseStrategy"/>.
    /// </summary>
    public string CategoryParseStrategy { get; private set; }

    /// <summary>
    /// Gets <see cref="RssFeed"/> categories.
    /// </summary>
    public IEnumerable<RssFeedCategoryItem> RssFeedCategories { get; private set; }

    /// <summary>
    /// Returns <see cref="RssFeed"/> to <see cref="RssFeedItem"/> projection.
    /// </summary>
    /// <returns><see cref="RssFeed"/> to <see cref="RssFeedItem"/> projection.</returns>
    public static Expression<Func<RssFeed, RssFeedItem>> GetProjection()
    {
        var rssFeedCategoryProjection = RssFeedCategoryItem.GetProjection();
        return rssFeed => new RssFeedItem
        {
            Id = rssFeed.Id,
            Url = rssFeed.Url,
            Category = rssFeed.Category!.Name,
            CategoryParseStrategy = rssFeed
                .CategoryParseConfiguration
                .CategoryParseStrategy
                .GetDisplayName(),
            RssFeedCategories = rssFeed
                .RssFeedCategories
                .AsQueryable()
                .Select(rssFeedCategoryProjection),
        };
    }
}
