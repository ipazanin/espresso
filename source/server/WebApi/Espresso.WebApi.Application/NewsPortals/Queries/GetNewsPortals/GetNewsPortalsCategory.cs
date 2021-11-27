// GetNewsPortalsCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using System.Linq.Expressions;

namespace Espresso.Application.NewsPortals;

public record GetNewsPortalsCategory
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Color { get; private set; } = string.Empty;

    public string? KeyWordsRegexPattern { get; private set; }

    public int? SortIndex { get; private set; }

    public int? Position { get; private set; }

    public CategoryType CategoryType { get; private set; }

    private GetNewsPortalsCategory()
    {
    }

    public static Expression<Func<Category, GetNewsPortalsCategory>> GetProjection()
    {
        return category => new GetNewsPortalsCategory
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            KeyWordsRegexPattern = category.KeyWordsRegexPattern,
            SortIndex = category.SortIndex,
            Position = category.Position,
            CategoryType = category.CategoryType,
        };
    }
}
