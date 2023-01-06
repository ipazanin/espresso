// GetNewsPortalsQueryNewsPortal.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortals;

public sealed class GetNewsPortalsQueryNewsPortal
{
    private GetNewsPortalsQueryNewsPortal()
    {
        NewsPortal = null!;
        Category = null!;
    }

    public NewsPortalDto NewsPortal { get; private set; }

    public CategoryDto Category { get; private set; }

    public static Expression<Func<NewsPortal, GetNewsPortalsQueryNewsPortal>> GetProjection()
    {
        var newsPortalProjection = NewsPortalDto.GetProjection().Compile();
        var categoryProjection = CategoryDto.Projection.Compile();

        return newsPortal => new GetNewsPortalsQueryNewsPortal
        {
            NewsPortal = newsPortalProjection.Invoke(newsPortal),
            Category = categoryProjection.Invoke(newsPortal.Category!),
        };
    }
}
