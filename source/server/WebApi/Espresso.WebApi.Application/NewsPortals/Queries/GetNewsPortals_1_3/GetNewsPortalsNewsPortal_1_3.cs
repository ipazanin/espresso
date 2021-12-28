// GetNewsPortalsNewsPortal_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals_1_3;
#pragma warning disable S101 // Types should be named in PascalCase
public class GetNewsPortalsNewsPortal_1_3
#pragma warning restore S101 // Types should be named in PascalCase
{
    /// <summary>
    /// Gets news Portal ID.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets news Portal Name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    public string IconUrl { get; private set; } = string.Empty;

#pragma warning disable SA1201 // Elements should appear in the correct order
    private GetNewsPortalsNewsPortal_1_3()
#pragma warning restore SA1201 // Elements should appear in the correct order
    {
    }

    public static Expression<Func<NewsPortal, GetNewsPortalsNewsPortal_1_3>> GetProjection()
    {
        return newsPortal => new GetNewsPortalsNewsPortal_1_3
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl,
        };
    }
}
