using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class Article :
        IEntity<Guid, Article>
    {
        #region Properties
        public Guid Id { get; private set; }

        public string ArticleId { get; private set; }

        public string Url { get; private set; }

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

        public bool IsHidden { get; private set; }

        public int NewsPortalId { get; private set; }

        public NewsPortal? NewsPortal { get; private set; }

        public int RssFeedId { get; private set; }

        public RssFeed? RssFeed { get; private set; }

        public ICollection<ArticleCategory> ArticleCategories { get; private set; } = new List<ArticleCategory>();
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
            ArticleId = null!;
            Url = null!;
            Summary = null!;
            Title = null!;
        }

        public Article(
            Guid id,
            string articleId,
            string url,
            string summary,
            string title,
            string? imageUrl,
            DateTime createDateTime,
            DateTime updateDateTime,
            DateTime publishDateTime,
            int numberOfClicks,
            decimal trendingScore,
            bool isHidden,
            int newsPortalId,
            int rssFeedId,
            IEnumerable<ArticleCategory>? articleCategories,
            NewsPortal? newsPortal,
            RssFeed? rssFeed
        )
        {
            Id = id;
            ArticleId = articleId;
            Url = url;
            Summary = summary;
            Title = title;
            ImageUrl = imageUrl;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            PublishDateTime = publishDateTime;
            NumberOfClicks = numberOfClicks;
            TrendingScore = trendingScore;
            IsHidden = isHidden;
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
        public void HideArticle()
        {
            IsHidden = true;
        }

        /// <summary>
        /// Updates Article if necessary
        /// </summary>
        /// <param name="other"></param>
        /// <returns>If article should be upadted</returns>
        public bool Update(Article other)
        {
            var shouldUpdate = false;
            if (!ArticleId.Equals(other.ArticleId))
            {
                ArticleId = other.ArticleId;
                shouldUpdate = true;
            }
            if (!Url.Equals(other.Url))
            {
                Url = other.Url;
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
        public static Expression<Func<Article, bool>> GetLatestArticlePredicate(
            IEnumerable<int>? categoryIds,
            IEnumerable<int>? newsPortalIds
        )
        {
            return article => !article.IsHidden &&
                (categoryIds == null || article
                    .ArticleCategories
                    .Any(articleCategory => categoryIds.Contains(articleCategory.CategoryId))) &&
                (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId));
        }

        public static Expression<Func<Article, bool>> GetCategoryArticlePredicate(
            int categoryId,
            IEnumerable<int>? newsPortalIds
        )
        {
            return article => !article.IsHidden &&
                        article.ArticleCategories
                            .Any(articleCategory => articleCategory.CategoryId.Equals(categoryId)) &&
                        (newsPortalIds == null || newsPortalIds.Contains(article.NewsPortalId));
        }

        public static Expression<Func<Article, bool>> GetTrendingArticlePredicate(TimeSpan maxAgeOfTrendingArticle)
        {
            var maxTrendingDateTime = DateTime.UtcNow - maxAgeOfTrendingArticle;

            return article => !article.IsHidden && article.PublishDateTime > maxTrendingDateTime;
        }

        public static Expression<Func<Article, object>> GetTrendingArticleOrderByDescendingExpression()
        {
            return article => article.TrendingScore;
        }

        public static Expression<Func<Article, object>> GetArticleOrderByDescendingExpression()
        {
            return article => article.PublishDateTime;
        }
        #endregion

        #endregion

        #region Inner Classes
        public const int ArticleIdMaxLength = 500;
        public const int SummaryMaxLength = 2000;
        public const int TitleMaxLength = 500;
        public const int UrlMaxLength = 500;
        public const int ImageUrlMaxLength = 500;

        public const bool IsHiddenDefaultValue = false;
        #endregion
    }
}
