// GetNewsPortalsRssFeedContentModifier.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.Application.NewsPortals;

public class GetNewsPortalsRssFeedContentModifier
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
