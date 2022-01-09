// Article.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.ValueObjects.ArticleValueObjects;

namespace Espresso.Domain.Entities;

public class Article
{
    public const int SummaryMaxLength = 2000;
    public const int TitleMaxLength = 500;
    public const int UrlMaxLength = 500;
    public const int WebUrlMaxLength = 500;
    public const int ImageUrlMaxLength = 500;
    public const decimal TrendingScoreDefaultValue = 0m;

    /// <summary>
    /// Initializes a new instance of the <see cref="Article"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="url"></param>
    /// <param name="webUrl"></param>
    /// <param name="summary"></param>
    /// <param name="title"></param>
    /// <param name="imageUrl"></param>
    /// <param name="createDateTime"></param>
    /// <param name="updateDateTime"></param>
    /// <param name="publishDateTime"></param>
    /// <param name="numberOfClicks"></param>
    /// <param name="trendingScore"></param>
    /// <param name="editorConfiguration"></param>
    /// <param name="newsPortalId"></param>
    /// <param name="rssFeedId"></param>
    /// <param name="articleCategories"></param>
    /// <param name="newsPortal"></param>
    /// <param name="rssFeed"></param>
    /// <param name="firstSimilarArticles"></param>
    /// <param name="secondSimilarArticles"></param>
    public Article(
        Guid id,
        string url,
        string webUrl,
        string summary,
        string title,
        string? imageUrl,
        DateTimeOffset createDateTime,
        DateTimeOffset updateDateTime,
        DateTimeOffset publishDateTime,
        int numberOfClicks,
        decimal trendingScore,
        EditorConfiguration editorConfiguration,
        int newsPortalId,
        int rssFeedId,
        IEnumerable<ArticleCategory>? articleCategories,
        NewsPortal? newsPortal,
        RssFeed? rssFeed,
        IEnumerable<SimilarArticle>? firstSimilarArticles,
        IEnumerable<SimilarArticle>? secondSimilarArticles)
    {
        Id = id;
        Url = url;
        WebUrl = webUrl;
        Summary = summary;
        Title = title;
        ImageUrl = imageUrl;
        CreateDateTime = createDateTime;
        UpdateDateTime = updateDateTime;
        PublishDateTime = publishDateTime;
        NumberOfClicks = numberOfClicks;
        TrendingScore = trendingScore;
        EditorConfiguration = editorConfiguration;
        NewsPortalId = newsPortalId;
        RssFeedId = rssFeedId;
        ArticleCategories = articleCategories?.ToList() ?? ArticleCategories;
        NewsPortal = newsPortal;
        RssFeed = rssFeed;
        FirstSimilarArticles = firstSimilarArticles?.ToList() ?? FirstSimilarArticles;
        SecondSimilarArticles = secondSimilarArticles?.ToList() ?? SecondSimilarArticles;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Article"/> class.
    /// ORM Constructor.
    /// </summary>
    private Article()
    {
        Url = null!;
        Summary = null!;
        Title = null!;
        WebUrl = null!;
        EditorConfiguration = null!;
    }

    public Guid Id { get; private set; }

    public string Url { get; private set; }

    public string WebUrl { get; private set; }

    public string Summary { get; private set; }

    public string Title { get; private set; }

    public string? ImageUrl { get; private set; }

    /// <summary>
    /// Gets date Time when article was created in Espresso App.
    /// </summary>
    public DateTimeOffset CreateDateTime { get; private set; }

    public DateTimeOffset UpdateDateTime { get; private set; }

    public DateTimeOffset PublishDateTime { get; private set; }

    public int NumberOfClicks { get; private set; }

    public decimal TrendingScore { get; private set; }

    public EditorConfiguration EditorConfiguration { get; private set; }

    public int NewsPortalId { get; private set; }

    public NewsPortal? NewsPortal { get; private set; }

    public int RssFeedId { get; private set; }

    public RssFeed? RssFeed { get; private set; }

    public ICollection<ArticleCategory> ArticleCategories { get; private set; } = new List<ArticleCategory>();

    public ICollection<SimilarArticle> FirstSimilarArticles { get; private set; } = new List<SimilarArticle>();

    public ICollection<SimilarArticle> SecondSimilarArticles { get; private set; } = new List<SimilarArticle>();

    /// <summary>
    ///
    /// </summary>
    public void IncrementNumberOfClicks()
    {
        NumberOfClicks++;
    }

    /// <summary>
    ///
    /// </summary>
    public void SetIsFeaturedValue(bool? isFeatured, int? featuredPosition)
    {
        EditorConfiguration = EditorConfiguration with
        {
            IsFeatured = isFeatured,
            FeaturedPosition = featuredPosition
        };
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="trendingScore"></param>
    public void UpdateTrendingScore(decimal trendingScore)
    {
        TrendingScore = trendingScore;
    }

    /// <summary>
    ///
    /// </summary>
    public void SetIsHidden(bool isHidden)
    {
        EditorConfiguration = EditorConfiguration with { IsHidden = isHidden };
    }

    /// <summary>
    /// Updates Article if necessary.
    /// </summary>
    /// <param name="other"></param>
    /// <returns>If article should be updated.</returns>
    public (
        bool isSaved,
        IEnumerable<ArticleCategory> articleCategoriesToCreate,
        IEnumerable<ArticleCategory> articleCategoriesToDelete,
        IEnumerable<string> modifiedProperties) Update(Article other)
    {
        var shouldUpdate = false;
        var modifiedProperties = new List<string>();
        if (!Url.Equals(other.Url))
        {
            Url = other.Url;
            shouldUpdate = true;
            modifiedProperties.Add(nameof(Url));
        }

        if (!WebUrl.Equals(other.WebUrl))
        {
            WebUrl = other.WebUrl;
            shouldUpdate = true;
            modifiedProperties.Add(nameof(WebUrl));
        }

        if (!Summary.Equals(other.Summary))
        {
            Summary = other.Summary;
            shouldUpdate = true;
            modifiedProperties.Add(nameof(Summary));
        }

        if (!Title.Equals(other.Title))
        {
            Title = other.Title;
            shouldUpdate = true;
            modifiedProperties.Add(nameof(Title));
        }

        if (!(ImageUrl is null ? other.ImageUrl is null : ImageUrl.Equals(other.ImageUrl)))
        {
            ImageUrl = other.ImageUrl;
            shouldUpdate = true;
            modifiedProperties.Add(nameof(ImageUrl));
        }

        var articleCategoriesToDelete = new List<ArticleCategory>();
        var articleCategoriesToCreate = new List<ArticleCategory>();

        foreach (var articleCategory in ArticleCategories)
        {
            if (
                !other.ArticleCategories.Any(otherArticleCategory => otherArticleCategory.CategoryId.Equals(articleCategory.CategoryId)))
            {
                articleCategoriesToDelete.Add(articleCategory);
                shouldUpdate = true;
            }
        }

        foreach (var articleCategory in articleCategoriesToDelete)
        {
            ArticleCategories.Remove(articleCategory);
        }

        foreach (var otherArticleCategory in other.ArticleCategories)
        {
            if (!ArticleCategories.Any(articleCategory => articleCategory.CategoryId.Equals(otherArticleCategory.CategoryId)))
            {
                var newArticleCategory = new ArticleCategory(
                    id: otherArticleCategory.Id,
                    articleId: Id,
                    categoryId: otherArticleCategory.CategoryId,
                    article: null,
                    category: null);
                articleCategoriesToCreate.Add(newArticleCategory);
                ArticleCategories.Add(newArticleCategory);
                shouldUpdate = true;
            }
        }

        if (shouldUpdate)
        {
            UpdateDateTime = DateTimeOffset.UtcNow;
        }

        var numberOfArticleCategoriesAfterUpdate = ArticleCategories.Count + articleCategoriesToCreate.Count - articleCategoriesToDelete.Count;
        if (numberOfArticleCategoriesAfterUpdate < 1)
        {
            articleCategoriesToDelete.Clear();
        }

        return (shouldUpdate, articleCategoriesToCreate, articleCategoriesToDelete, modifiedProperties);
    }

    public void UpdateNewsPortalAndArticlecategories(
        NewsPortal newsPortal,
        IEnumerable<ArticleCategory> articleCategories)
    {
        NewsPortal = newsPortal;
        ArticleCategories = articleCategories.ToList();
    }

    public void UpdateArticleCategories(
        IEnumerable<ArticleCategory> articleCategories)
    {
        ArticleCategories = ArticleCategories
            .Union(articleCategories.Select(articleCategory => new ArticleCategory(
                id: articleCategory.Id,
                articleId: Id,
                categoryId: articleCategory.CategoryId,
                article: null,
                category: articleCategory.Category)))
            .ToList();
    }

    public void SetNewsPortal(NewsPortal newsPortal)
    {
        NewsPortal = newsPortal;
        NewsPortalId = newsPortal.Id;
    }

    public void SetRssFeed(RssFeed rssFeed)
    {
        RssFeed = rssFeed;
        RssFeedId = rssFeed.Id;
    }

    public IEnumerable<SimilarArticle> GetSimilarArticles()
    {
        return FirstSimilarArticles.Union(SecondSimilarArticles);
    }
}
