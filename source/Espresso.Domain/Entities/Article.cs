using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Extensions;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Infrastructure;
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
        #endregion

        #region NotMapped In Database
        public IEnumerable<ArticleCategory> CreateArticleCategories { get; private set; } = new List<ArticleCategory>();
        public IEnumerable<ArticleCategory> DeleteArticleCategories { get; private set; } = new List<ArticleCategory>();
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
            RssFeed? rssFeed
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
        public bool Update(Article other)
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

            CreateArticleCategories = new List<ArticleCategory>();
            DeleteArticleCategories = new List<ArticleCategory>();
            var articleCategoriesToDelete = new List<ArticleCategory>();

            foreach (var articleCategory in ArticleCategories)
            {
                if (
                    !other.ArticleCategories.Any(otherArticleCategory => otherArticleCategory.CategoryId.Equals(articleCategory.CategoryId))
                )
                {
                    articleCategoriesToDelete.Add(articleCategory);
                    DeleteArticleCategories = DeleteArticleCategories.Append(articleCategory);
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
                    CreateArticleCategories = CreateArticleCategories.Append(newArticleCategory);
                    ArticleCategories.Add(newArticleCategory);
                    shouldUpdate = true;
                }
            }

            return shouldUpdate;
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

        #region Expressions
        public static Expression<Func<Article, bool>> GetFilteredArticlesPredicate(
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds,
            string? titleSearchQuery,
            DateTime? articleCreateDateTime
        )
        {
            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;

            return article =>
                !article.EditorConfiguration.IsHidden &&
                article.CreateDateTime <= articleMinimumAge &&
                (categoryIds == null || article
                    .ArticleCategories
                    .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId)) &&
                (string.IsNullOrEmpty(titleSearchQuery) || article.Title.Contains(titleSearchQuery));
        }

        public static Expression<Func<Article, bool>> GetFilteredArticlesPredicate(
            int categoryId,
            IEnumerable<int>? newsPortalIds,
            string? titleSearchQuery,
            DateTime? articleCreateDateTime
        )
        {
            var articleMinimumAge = articleCreateDateTime ?? DateTime.UtcNow;

            return article =>
                !article.EditorConfiguration.IsHidden &&
                article.CreateDateTime <= articleMinimumAge &&
                article.ArticleCategories.Any(articleCategory => articleCategory.CategoryId.Equals(categoryId)) &&
                (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId)) &&
                (string.IsNullOrEmpty(titleSearchQuery) || article.Title.Contains(titleSearchQuery));
        }

        public static Expression<Func<Article, bool>> GetFilteredFeaturedArticlesPredicate(
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds,
            string? titleSearchQuery,
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
                (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId)) &&
                !article.NewsPortal!.CategoryId.Equals((int)CategoryId.Local) &&
                (string.IsNullOrEmpty(titleSearchQuery) || article.Title.Contains(titleSearchQuery));
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

        public static Expression<Func<Article, bool>> GetAutocompleteArticleTitleExpression(
            string? titleSearchQuery
        )
        {
            if (string.IsNullOrWhiteSpace(titleSearchQuery))
            {
                return article => false;
            }
            var keywords = titleSearchQuery.RemoveExtraWhiteSpaceCharacters().Split(" ");

            return article => keywords.Any(keyword => article.Title.StartsWith(keyword, StringComparison.InvariantCultureIgnoreCase)) &&
                keywords.Any(keyword => article.Title.Contains($" {keyword}", StringComparison.InvariantCultureIgnoreCase));
        }

        public static Expression<Func<Article, object?>> GetOrderByFeaturedArticlesExpression()
        {
            return article => article.EditorConfiguration.FeaturedPosition;
        }
        #endregion

        #endregion
    }
}
