// GetNewsPortalsRssFeedContentModifier.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;

public sealed class GetNewsPortalsRssFeedContentModifier
{
    private GetNewsPortalsRssFeedContentModifier()
    {
    }

    public int Id { get; private set; }

    public string SourceValue { get; private set; } = string.Empty;

    public string ReplacementValue { get; private set; } = string.Empty;

    public static Expression<Func<RssFeedContentModifier, GetNewsPortalsRssFeedContentModifier>> GetProjection()
    {
        return rssFeedCategory => new GetNewsPortalsRssFeedContentModifier
        {
            Id = rssFeedCategory.Id,
            SourceValue = rssFeedCategory.SourceValue,
            ReplacementValue = rssFeedCategory.ReplacementValue,
        };
    }
}
