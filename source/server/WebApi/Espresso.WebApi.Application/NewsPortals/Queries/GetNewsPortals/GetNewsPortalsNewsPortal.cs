// GetNewsPortalsNewsPortal.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Application.NewsPortals;
using Espresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;

public class GetNewsPortalsNewsPortal
{
    private GetNewsPortalsNewsPortal()
    {
    }

    /// <summary>
    /// Gets news Portal ID.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets news Portal Name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    public string IconUrl { get; private set; } = string.Empty;

    public GetNewsPortalsCategory Category { get; private set; } = null!;

    public GetNewsPortalsRegion Region { get; private set; } = null!;

    public IEnumerable<GetNewsPortalsRssFeed> RssFeeds { get; private set; } = new List<GetNewsPortalsRssFeed>();

    public static Expression<Func<NewsPortal, GetNewsPortalsNewsPortal>> GetProjection()
    {
        var categoryProjection = GetNewsPortalsCategory.GetProjection().Compile();
        var regionProjection = GetNewsPortalsRegion.GetProjection().Compile();
        var rssFeedProjection = GetNewsPortalsRssFeed.GetProjection();
        return newsPortal => new GetNewsPortalsNewsPortal
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl,
            Category = categoryProjection.Invoke(newsPortal.Category!),
            RssFeeds = newsPortal
                .RssFeeds
                .AsQueryable()
                .Select(rssFeedProjection),
            Region = regionProjection.Invoke(newsPortal.Region!),
        };
    }

    public static IQueryable<NewsPortal> Include(IQueryable<NewsPortal> newsPortals)
    {
        return newsPortals.Include(newsPortal => newsPortal.Region)
            .Include(newsPortal => newsPortal.Category)
            .Include(newsPortal => newsPortal.RssFeeds)
            .ThenInclude(rssFeed => rssFeed.RssFeedCategories)
            .Include(newsPortal => newsPortal.RssFeeds)
            .ThenInclude(rssFeed => rssFeed.RssFeedContentModifiers);
    }
}
