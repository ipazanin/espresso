// CategoryDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;

public class CategoryDto
{
    public CategoryDto(
        int id,
        string name,
        string color,
        string? keyWordsRegexPattern,
        int? sortIndex,
        int? position,
        CategoryType categoryType,
        string url)
    {
        Id = id;
        Name = name;
        Color = color;
        KeyWordsRegexPattern = keyWordsRegexPattern;
        SortIndex = sortIndex;
        Position = position;
        CategoryType = categoryType;
        Url = url;
    }

    private CategoryDto()
    {
    }

    public static Expression<Func<Category, CategoryDto>> Projection
    {
        get => category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            KeyWordsRegexPattern = category.KeyWordsRegexPattern,
            SortIndex = category.SortIndex,
            Position = category.Position,
            CategoryType = category.CategoryType,
            Url = category.Url,
        };
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public string? KeyWordsRegexPattern { get; set; }

    public int? SortIndex { get; set; }

    public int? Position { get; set; }

    public CategoryType CategoryType { get; set; }

    public string Url { get; set; } = string.Empty;

    public Category CreateCategory()
    {
        return new Category(
            id: Id,
            name: Name,
            color: Color,
            keyWordsRegexPattern: KeyWordsRegexPattern,
            sortIndex: SortIndex,
            position: Position,
            categoryType: CategoryType,
            categoryUrl: Url);
    }
}
