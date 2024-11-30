// GetNewsPortalsRssFeed.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;

public sealed class GetNewsPortalsRssFeed
{
    private GetNewsPortalsRssFeed()
    {
    }

    public int Id { get; private set; }

    public string Url { get; private set; } = string.Empty;

    public IEnumerable<GetNewsPortalsRssFeedContentModifier> RssFeedContentModifiers { get; private set; } = [];

    public IEnumerable<GetNewsPortalsRssFeedCategory> RssFeedCategories { get; private set; } = [];

    public static Expression<Func<RssFeed, GetNewsPortalsRssFeed>> GetProjection()
    {
        var rssFeedContentModifierProjection = GetNewsPortalsRssFeedContentModifier.GetProjection();
        var rssFeedCategoryProjection = GetNewsPortalsRssFeedCategory.GetProjection();
        return rssFeed => new GetNewsPortalsRssFeed
        {
            Id = rssFeed.Id,
            Url = rssFeed.Url,
            RssFeedContentModifiers = rssFeed.RssFeedContentModifiers
                .AsQueryable()
                .Select(rssFeedContentModifierProjection),
            RssFeedCategories = rssFeed.RssFeedCategories
                .AsQueryable()
                .Select(rssFeedCategoryProjection),
        };
    }
}
