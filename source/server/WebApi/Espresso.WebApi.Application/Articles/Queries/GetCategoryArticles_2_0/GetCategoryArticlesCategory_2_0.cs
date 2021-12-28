// GetCategoryArticlesCategory_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0;
#pragma warning disable S101 // Types should be named in PascalCase
public record GetCategoryArticlesCategory_2_0
#pragma warning restore S101 // Types should be named in PascalCase
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

    private GetCategoryArticlesCategory_2_0()
    {
    }

    public static Expression<Func<Category, GetCategoryArticlesCategory_2_0>> GetProjection()
    {
        return category => new GetCategoryArticlesCategory_2_0
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            Position = category.Position,
            CategoryType = category.CategoryType,
        };
    }
}
