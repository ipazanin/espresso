// Category.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Enums.CategoryEnums;

#pragma warning disable RCS1170

namespace Espresso.Domain.Entities;

public class Category
{
    public const int NameHasMaxLenght = 20;

    public const int ColorHasMaxLenght = 20;

    public const int KeyWordsRegexPatterHasMaxLenght = 1000;

    public const int UrlHasMaxLength = 20;

    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="color"></param>
    /// <param name="keyWordsRegexPattern"></param>
    /// <param name="sortIndex"></param>
    /// <param name="position"></param>
    /// <param name="categoryType"></param>
    /// <param name="categoryUrl"></param>
    public Category(
        int id,
        string name,
        string color,
        string? keyWordsRegexPattern,
        int? sortIndex,
        int? position,
        CategoryType categoryType,
        string categoryUrl)
    {
        Id = id;
        Name = name;
        Color = color;
        KeyWordsRegexPattern = keyWordsRegexPattern;
        SortIndex = sortIndex;
        Position = position;
        CategoryType = categoryType;
        Url = categoryUrl;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// ORM Constructor.
    /// </summary>
    private Category()
    {
        Name = null!;
        Color = null!;
        Url = null!;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Color { get; private set; }

    public string? KeyWordsRegexPattern { get; private set; }

    public int? SortIndex { get; private set; }

    public int? Position { get; private set; }

    public CategoryType CategoryType { get; private set; }

    public string Url { get; private set; }

    public ICollection<ArticleCategory> ArticleCategories { get; private set; } = new List<ArticleCategory>();

    public ICollection<RssFeed> RssFeeds { get; private set; } = new List<RssFeed>();

    public ICollection<NewsPortal> NewsPortals { get; private set; } = new List<NewsPortal>();

    public static Expression<Func<Category, bool>> GetAllCategoriesExceptGeneralExpression()
    {
        return category => !category.Id.Equals((int)CategoryId.General);
    }

    public static Expression<Func<Category, bool>> GetAllCategoriesExceptLocalExpression()
    {
        return category => !category.Id.Equals((int)CategoryId.Local);
    }
}
