using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Extensions;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.Utilities;
using Espresso.Domain.ValueObjects.ArticleValueObjects;

namespace Espresso.Domain.Entities
{
    public class Article :
        IEntity<Guid, Article>
    {

        #region Constants
        public const int SummaryMaxLength = 2000;
        public const int TitleMaxLength = 500;
        public const int UrlMaxLength = 500;
        public const int WebUrlMaxLength = 500;
        public const int ImageUrlMaxLength = 500;

        public const decimal TrendingScoreDefaultValue = 0m;
        #endregion

        #region Properties
        public Guid Id { get; private set; }

        public string Url { get; private set; }

        public string WebUrl { get; private set; }

        public string Summary { get; private set; }

        public string Title { get; private set; }

        public string? ImageUrl { get; private set; }

        /// <summary>
        /// Date Time when article was created in Espresso App
        /// </summary> 
        public DateTime CreateDateTime { get; private set; }

        public DateTime UpdateDateTime { get; private set; }

        public DateTime PublishDateTime { get; private set; }

        public int NumberOfClicks { get; private set; }

        public decimal TrendingScore { get; private set; }

        public EditorConfiguration EditorConfiguration { get; private set; }

        #region Relationships
        public int NewsPortalId { get; private set; }

        public NewsPortal? NewsPortal { get; private set; }

        public int RssFeedId { get; private set; }

        public RssFeed? RssFeed { get; private set; }

        public ICollection<ArticleCategory> ArticleCategories { get; private set; } = new List<ArticleCategory>();

        public ICollection<SimilarArticle> SubordinateArticles { get; private set; } = new List<SimilarArticle>();

        public SimilarArticle? MainArticle { get; private set; }
        #endregion

        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private Article()
        {
            Url = null!;
            Summary = null!;
            Title = null!;
            WebUrl = null!;
            EditorConfiguration = null!;
        }

        public Article(
            Guid id,
            string url,
            string webUrl,
            string summary,
            string title,
            string? imageUrl,
            DateTime createDateTime,
            DateTime updateDateTime,
            DateTime publishDateTime,
            int numberOfClicks,
            decimal trendingScore,
            EditorConfiguration editorConfiguration,
            int newsPortalId,
            int rssFeedId,
            IEnumerable<ArticleCategory>? articleCategories,
            NewsPortal? newsPortal,
            RssFeed? rssFeed,
            IEnumerable<SimilarArticle>? subordinateArticles,
            SimilarArticle? mainArticle
        )
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
            SubordinateArticles = subordinateArticles?.ToList() ?? SubordinateArticles;
            MainArticle = mainArticle;
        }
        #endregion

        #region Methods
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
        /// Updates Article if necessary
        /// </summary>
        /// <param name="other"></param>
        /// <returns>If article should be upadted</returns>
        public
        (
            bool shouldUpdate,
            IEnumerable<ArticleCategory> articleCategoriesToCreate,
            IEnumerable<ArticleCategory> articleCategoriesToDelete
        )
        Update(Article other)
        {
            var shouldUpdate = false;
            if (!Url.Equals(other.Url))
            {
                Url = other.Url;
                shouldUpdate = true;
            }
            if (!WebUrl.Equals(other.WebUrl))
            {
                WebUrl = other.WebUrl;
                shouldUpdate = true;
            }
            if (!Summary.Equals(other.Summary))
            {
                Summary = other.Summary;
                shouldUpdate = true;
            }
            if (!Title.Equals(other.Title))
            {
                Title = other.Title;
                shouldUpdate = true;
            }
            if (!(ImageUrl is null ? other.ImageUrl is null : ImageUrl.Equals(other.ImageUrl)))
            {
                ImageUrl = other.ImageUrl;
                shouldUpdate = true;
            }

            var articleCategoriesToDelete = new List<ArticleCategory>();
            var articleCategoriesToCreate = new List<ArticleCategory>();

            foreach (var articleCategory in ArticleCategories)
            {
                if (
                    !other.ArticleCategories.Any(otherArticleCategory => otherArticleCategory.CategoryId.Equals(articleCategory.CategoryId))
                )
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
                if (
                    !ArticleCategories.Any(articleCategory => articleCategory.CategoryId.Equals(otherArticleCategory.CategoryId))
                )
                {
                    var newArticleCategory = new ArticleCategory(
                        id: Guid.NewGuid(),
                        articleId: Id,
                        categoryId: otherArticleCategory.CategoryId,
                        article: null,
                        category: otherArticleCategory.Category
                    );
                    articleCategoriesToCreate.Add(newArticleCategory);
                    ArticleCategories.Add(newArticleCategory);
                    shouldUpdate = true;
                }
            }

            return (shouldUpdate, articleCategoriesToCreate, articleCategoriesToDelete);
        }

        public void UpdateNewsPortalAndArticlecategories(
            NewsPortal newsPortal,
            IEnumerable<ArticleCategory> articleCategories
        )
        {
            NewsPortal = newsPortal;
            ArticleCategories = articleCategories.ToList();
        }

        public void UpdateArticleCategories(
            IEnumerable<ArticleCategory> articleCategories
        )
        {
            ArticleCategories = ArticleCategories
                .Union(articleCategories.Select(articleCategory => new ArticleCategory(
                    id: articleCategory.Id,
                    articleId: Id,
                    categoryId: articleCategory.CategoryId,
                    article: null,
                    category: articleCategory.Category
                )))
                .ToList();
        }

        public void SetMainArticle(SimilarArticle mainArticle)
        {
            MainArticle = mainArticle;
        }

        #region Expressions
        public static Expression<Func<Article, bool>> GetFilteredLatestArticlesPredicate(
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds,
            string? titleSearchTerm,
            DateTime? articleCreateDateTime
        )
        {
            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;
            var searchTerms = LanguageUtility.GetSearchTerms(titleSearchTerm);

            return article =>
                !article.EditorConfiguration.IsHidden &&
                article.CreateDateTime <= articleMinimumAge &&
                article.MainArticle == null &&
                (categoryIds == null || article
                    .ArticleCategories
                    .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId)) &&
                (
                    searchTerms == null ||
                    searchTerms
                        .All(searchTerm => article
                            .Title
                            .ReplaceCroatianCharacters()
                            .Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
                        )
                );
        }

        public static Expression<Func<Article, bool>> GetFilteredLatestArticlesPredicate_2_0(
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds,
            string? titleSearchTerm,
            DateTime? articleCreateDateTime
        )
        {
            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;
            var searchTerms = LanguageUtility.GetSearchTerms(titleSearchTerm);

            return article =>
                !article.EditorConfiguration.IsHidden &&
                article.CreateDateTime <= articleMinimumAge &&
                (categoryIds == null || article
                    .ArticleCategories
                    .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId)) &&
                (
                    searchTerms == null ||
                    searchTerms
                        .All(searchTerm => article
                            .Title
                            .ReplaceCroatianCharacters()
                            .Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
                        )
                );
        }

        public static Expression<Func<Article, bool>> GetFilteredCategoryArticlesPredicate(
            int categoryId,
            IEnumerable<int>? newsPortalIds,
            string? searchTerm,
            DateTime? articleCreateDateTime
        )
        {
            return GetFilteredLatestArticlesPredicate_2_0(
                categoryIds: new List<int> { categoryId },
                newsPortalIds: newsPortalIds,
                titleSearchTerm: searchTerm,
                articleCreateDateTime: articleCreateDateTime
            );
        }

        public static Expression<Func<Article, bool>> GetFilteredFeaturedArticlesPredicate(
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds,
            TimeSpan maxAgeOfFeaturedArticle,
            DateTime? articleCreateDateTime
        )
        {
            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;
            var maxDateTimeOfFeaturedArticle = DateTime.UtcNow - maxAgeOfFeaturedArticle;

            return article =>
                !article.EditorConfiguration.IsHidden &&
                article.EditorConfiguration.IsFeatured == true &&
                article.CreateDateTime <= articleMinimumAge &&
                article.PublishDateTime > maxDateTimeOfFeaturedArticle &&
                (categoryIds == null || article
                    .ArticleCategories
                    .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
            // !article.NewsPortal!.CategoryId.Equals((int)CategoryId.Local) &&
                (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId));
        }


        public static Expression<Func<Article, bool>> GetTrendingArticlePredicate(
            TimeSpan maxAgeOfTrendingArticle,
            DateTime? articleCreateDateTime
        )
        {
            var maxTrendingDateTime = DateTime.UtcNow - maxAgeOfTrendingArticle;

            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;

            return article =>
                !article.EditorConfiguration.IsHidden &&
                article.EditorConfiguration.IsFeatured != false &&
                article.CreateDateTime <= articleMinimumAge &&
                article.PublishDateTime > maxTrendingDateTime &&
                !article.NewsPortal!.CategoryId.Equals((int)CategoryId.Local);
        }

        public static Expression<Func<Article, object>> GetOrderByDescendingTrendingScoreExpression()
        {
            return article => article.TrendingScore;
        }

        public static Expression<Func<Article, object>> GetOrderByDescendingPublishDateExpression()
        {
            return article => article.PublishDateTime;
        }

        public static Expression<Func<Article, object?>> GetOrderByFeaturedArticlesExpression()
        {
            return article => article.EditorConfiguration.FeaturedPosition == null ?
                int.MaxValue :
                article.EditorConfiguration.FeaturedPosition;
        }
        #endregion

        #endregion
    }
}
