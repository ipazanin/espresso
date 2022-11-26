// GetTrendingArticlesCategory.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;

public record GetTrendingArticlesCategory
{
    /// <summary>
    /// Gets category ID.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets category Name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    public string Color { get; private set; } = string.Empty;

    public int? Position { get; private set; }

    public CategoryType CategoryType { get; private set; }

    private GetTrendingArticlesCategory()
    {
    }

    public static Expression<Func<Category, GetTrendingArticlesCategory>> GetProjection()
    {
        return category => new GetTrendingArticlesCategory
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            Position = category.Position,
            CategoryType = category.CategoryType,
        };
    }
}
