// GetNewsPortalsCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals;

public class GetNewsPortalsCategory
{
    private GetNewsPortalsCategory()
    {
        Name = null!;
        Color = null!;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Color { get; private set; }

    public static Expression<Func<Category, GetNewsPortalsCategory>> GetProjection()
    {
        return category => new GetNewsPortalsCategory
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
        };
    }
}
