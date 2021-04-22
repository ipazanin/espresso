using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <returns>If article should be updated</returns>
        public (
            bool isSaved,
            IEnumerable<ArticleCategory> articleCategoriesToCreate,
            IEnumerable<ArticleCategory> articleCategoriesToDelete,
            IEnumerable<string> modifiedProperties
        ) Update(Article other)
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
                if (!ArticleCategories.Any(articleCategory => articleCategory.CategoryId.Equals(otherArticleCategory.CategoryId)))
                {
                    var newArticleCategory = new ArticleCategory(
                        id: otherArticleCategory.Id,
                        articleId: Id,
                        categoryId: otherArticleCategory.CategoryId,
                        article: null,
                        category: null
                    );
                    articleCategoriesToCreate.Add(newArticleCategory);
                    ArticleCategories.Add(newArticleCategory);
                    shouldUpdate = true;
                }
            }

            if (shouldUpdate)
            {
                UpdateDateTime = DateTime.UtcNow;
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

        public void RemoveSimilarArticles()
        {
            if (
                MainArticle is not null &&
                MainArticle.MainArticle is not null
            )
            {
                MainArticle.MainArticle.RemoveSubordinateArticle(this);
                MainArticle = null;
            }
            foreach (var subordinateArticle in SubordinateArticles)
            {
                subordinateArticle.SubordinateArticle?.RemoveMainArticle();
            }
            SubordinateArticles.Clear();
        }

        private void RemoveSubordinateArticle(Article subordinateArticle)
        {
            SubordinateArticles = SubordinateArticles
                .Where(similarArticle => similarArticle.SubordinateArticleId != subordinateArticle.Id)
                .ToList();
        }

        private void RemoveMainArticle()
        {
            MainArticle = null;
        }
        #endregion
    }
}
