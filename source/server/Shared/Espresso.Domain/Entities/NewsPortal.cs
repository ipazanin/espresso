// NewsPortal.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Linq.Expressions;

#pragma warning disable RCS1170 // Use read-only auto-implemented property.

namespace Espresso.Domain.Entities;

/// <summary>
/// Represents news source, for example www.index.hr.
/// </summary>
public class NewsPortal
{
    public const bool IsEnabledDefaultValue = true;

    public const int NameMaxLength = 100;

    public const int BaseUrlMaxLength = 100;

    public const int IconUrlMaxlength = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="NewsPortal"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="baseUrl"></param>
    /// <param name="iconUrl"></param>
    /// <param name="isNewOverride"></param>
    /// <param name="createdAt"></param>
    /// <param name="categoryId"></param>
    /// <param name="regionId"></param>
    /// <param name="isEnabled"></param>
    public NewsPortal(
        int id,
        string name,
        string baseUrl,
        string iconUrl,
        bool? isNewOverride,
        DateTimeOffset createdAt,
        int categoryId,
        int regionId,
        bool isEnabled)
    {
        Id = id;
        Name = name;
        BaseUrl = baseUrl;
        IconUrl = iconUrl;
        IsNewOverride = isNewOverride;
        CreatedAt = createdAt;
        CategoryId = categoryId;
        RegionId = regionId;
        IsEnabled = isEnabled;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NewsPortal"/> class.
    /// </summary>
    /// <remarks>
    /// ORM Constructor.
    /// </remarks>
    private NewsPortal()
    {
        Name = null!;
        BaseUrl = null!;
        IconUrl = null!;
    }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> id.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> name.
    /// </summary>
    public string BaseUrl { get; private set; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> icon URL.
    /// </summary>
    public string IconUrl { get; private set; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> is new override.
    /// </summary>
    /// <remarks>
    /// <see cref="IsNewOverride"/> tells applications if news portal is considered new regardless of time when it was added.
    /// </remarks>
    public bool? IsNewOverride { get; private set; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> created at.
    /// </summary>
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>
    /// Gets a value indicating whether <see cref="NewsPortal"/> is used in application for parsing <see cref="RssFeeds"/>.
    /// </summary>
    public bool IsEnabled { get; private set; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> region id.
    /// </summary>
    public int RegionId { get; private set; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> <see cref="Entities.Region"/>.
    /// </summary>
    public Region? Region { get; private set; }

    public int CategoryId { get; private set; }

    public Category? Category { get; private set; }

    public ICollection<RssFeed> RssFeeds { get; private set; } = new List<RssFeed>();

    public ICollection<Article> Articles { get; private set; } = new List<Article>();

    public static Expression<Func<NewsPortal, bool>> GetCategorySugestedNewsPortalsPredicate(
        IEnumerable<int>? newsPortalIds,
        int categoryId,
        int? regionId,
        TimeSpan maxAgeOfNewNewsPortal)
    {
        var newNewsPortalMinDate = DateTimeOffset.UtcNow - maxAgeOfNewNewsPortal;
        return newsPortal =>
            newsPortalIds != null && !newsPortalIds.Contains(newsPortal.Id) &&
            categoryId.Equals(newsPortal.CategoryId) &&
            (regionId == null || newsPortal.RegionId == regionId) &&
            (
                newsPortal.IsNewOverride ?? newsPortal.CreatedAt > newNewsPortalMinDate);
    }

    public static Expression<Func<NewsPortal, bool>> GetLatestSugestedNewsPortalsPredicate(
        IEnumerable<int>? newsPortalIds,
        IEnumerable<int>? categoryIds,
        TimeSpan maxAgeOfNewNewsPortal)
    {
        var newNewsPortalMinDate = DateTimeOffset.UtcNow - maxAgeOfNewNewsPortal;

        return newsPortal =>
            newsPortalIds != null && !newsPortalIds.Contains(newsPortal.Id) &&
            (categoryIds == null || categoryIds.Contains(newsPortal.CategoryId)) &&
            (!newsPortal.CategoryId.Equals((int)Enums.CategoryEnums.CategoryId.Local)) &&
            (
                newsPortal.IsNewOverride ?? newsPortal.CreatedAt > newNewsPortalMinDate);
    }
}
