using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class Article :
        IEntity<Guid, Article>
    {
        #region Constants
        public const int ArticleIdMaxLength = 500;
        public const int SummaryMaxLength = 2000;
        public const int TitleMaxLength = 500;
        public const int UrlMaxLength = 500;
        public const int ImageUrlMaxLength = 500;

        public const bool ArticleIdIsRequired = true;
        public const bool SummaryIsRequired = true;
        public const bool TitleIsRequired = true;
        public const bool UrlIsRequired = true;
        public const bool ImageUrlIsRequired = false;
        #endregion

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

        public int NewsPortalId { get; private set; }

        public NewsPortal? NewsPortal { get; private set; }

        public int RssFeedId { get; private set; }

        public RssFeed? RssFeed { get; private set; }

        public ICollection<ArticleCategory> ArticleCategories { get; private set; } = new List<ArticleCategory>();

        public static Expression<Func<Article, Article>> Projection => article => article;

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
            NewsPortalId = newsPortalId;
            RssFeedId = rssFeedId;
            ArticleCategories = articleCategories?.ToList() ?? ArticleCategories;
            NewsPortal = newsPortal;
            RssFeed = rssFeed;
        }
        #endregion

        #region Methods
        public void IncrementNumberOfClicks()
        {
            NumberOfClicks++;
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

            foreach (var articleCategory in ArticleCategories)
            {
                if (
                    !other.ArticleCategories.Any(otherArticleCategory => otherArticleCategory.CategoryId.Equals(articleCategory.CategoryId))
                )
                {
                    DeleteArticleCategories = DeleteArticleCategories.Append(articleCategory);
                    shouldUpdate = true;
                }
            }

            foreach (var otherArticleCategory in other.ArticleCategories)
            {
                if (
                    !ArticleCategories.Any(articleCategory => articleCategory.CategoryId.Equals(otherArticleCategory.CategoryId))
                )
                {
                    CreateArticleCategories = CreateArticleCategories.Append(new ArticleCategory(
                        id: otherArticleCategory.Id,
                        articleId: Id,
                        categoryId: otherArticleCategory.CategoryId,
                        article: null,
                        category: otherArticleCategory.Category
                    ));
                    shouldUpdate = true;
                }
            }

            ArticleCategories = other
                .ArticleCategories
                .Select(otherArticleCategory => new ArticleCategory(
                    id: otherArticleCategory.Id,
                    articleId: Id,
                    categoryId: otherArticleCategory.CategoryId,
                    article: null,
                    category: otherArticleCategory.Category
                ))
                .ToList();

            return shouldUpdate;
        }

        public void UpdateNewsPortalAndArticlecategories(NewsPortal newsPortal, IEnumerable<ArticleCategory> articleCategories)
        {
            NewsPortal = newsPortal;
            ArticleCategories = articleCategories.ToList();
        }

        public void UpdateArticleCategories(IEnumerable<ArticleCategory> articleCategories)
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
        #endregion
    }
}
