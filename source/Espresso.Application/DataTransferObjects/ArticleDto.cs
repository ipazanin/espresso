using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects
{
    public class ArticleDto
    {
        #region Properties

        /// <summary>
        /// ID created by app
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Article Url provided by RSS Feed
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Article Title Parsed from RSS Feed
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Image URL parsed from src attribute of first img element or second rss feed link, first is 
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Article Publish time provided by RSS Feed
        /// </summary>
        public DateTime PublishDateTime { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public decimal TrendingScore { get; set; }

        public int NumberOfClicks { get; set; }

        public string ArticleId { get; set; }

        public string Summary { get; set; }

        public int RssFeedId { get; set; }

        public bool IsHidden { get; set; }

        /// <summary>
        /// News Portal ID
        /// </summary>
        public NewsPortalDto NewsPortal { get; set; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        #endregion

        #region Constructors
        /// <summary>
        /// Used by JSON serializer
        /// </summary>
        public ArticleDto()
        {
            Url = null!;
            Title = null!;
            ArticleId = null!;
            Summary = null!;
            NewsPortal = null!;
        }

        public ArticleDto(
            Guid id,
            string url,
            string title,
            string? imageUrl,
            DateTime publishDateTime,
            DateTime createDateTime,
            DateTime updateDateTime,
            decimal trendingScore,
            int numberOfClicks,
            string articleId,
            string summary,
            bool isHidden,
            int rssFeedId,
            NewsPortalDto newsPortal,
            IEnumerable<CategoryDto> categories
        )
        {
            Id = id;
            Url = url;
            Title = title;
            ImageUrl = imageUrl;
            PublishDateTime = publishDateTime;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            TrendingScore = trendingScore;
            NumberOfClicks = numberOfClicks;
            ArticleId = articleId;
            Summary = summary;
            IsHidden = isHidden;
            RssFeedId = rssFeedId;
            NewsPortal = newsPortal;
            Categories = categories;
        }

        #endregion

        #region Projections
        public static Expression<Func<Article, ArticleDto>> Projection => article => new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            ImageUrl = article.ImageUrl,
            Url = article.Url,
            PublishDateTime = article.PublishDateTime,
            CreateDateTime = article.CreateDateTime,
            UpdateDateTime = article.UpdateDateTime,
            NewsPortal = NewsPortalDto.Projection.Compile().Invoke(article.NewsPortal!),
            Categories = article.ArticleCategories
                .AsQueryable()
                .Select(articleCategory => articleCategory.Category)
                .Select(CategoryDto.Projection!),
            TrendingScore = article.TrendingScore,
            NumberOfClicks = article.NumberOfClicks,
            ArticleId = article.ArticleId,
            Summary = article.Summary,
            RssFeedId = article.RssFeedId,
            IsHidden = article.IsHidden,
        };

        public static Func<ArticleDto, Article> ToArticleProjection => article =>
        {
            var articleCategories = article.Categories.Select(category => new ArticleCategory(
                id: new Guid(),
                articleId: article.Id,
                categoryId: category.Id,
                article: null,
                category: new Category(
                    id: category.Id,
                    name: category.Name,
                    color: category.Color,
                    keyWordsRegexPattern: category.KeyWordsRegexPattern,
                    sortIndex: category.SortIndex
                )
            )).ToList();

            var newsPortal = new NewsPortal(
                id: article.NewsPortal.Id,
                name: article.NewsPortal.Name,
                baseUrl: article.NewsPortal.BaseUrl,
                iconUrl: article.NewsPortal.IconUrl,
                isNewOverride: article.NewsPortal.IsNewOverride,
                createdAt: article.NewsPortal.CreatedAt,
                categoryId: article.NewsPortal.CategoryId
            );

            var createdArticle = new Article(
                id: article.Id,
                articleId: article.ArticleId,
                url: article.Url,
                summary: article.Summary,
                title: article.Title,
                imageUrl: article.ImageUrl,
                createDateTime: article.CreateDateTime,
                updateDateTime: article.UpdateDateTime,
                publishDateTime: article.PublishDateTime,
                numberOfClicks: article.NumberOfClicks,
                trendingScore: article.TrendingScore,
                isHidden: article.IsHidden,
                newsPortalId: article.NewsPortal.Id,
                rssFeedId: article.RssFeedId,
                articleCategories: articleCategories,
                newsPortal: newsPortal,
                rssFeed: null
            );
            return createdArticle;
        };
        #endregion

        #region Methods
        public override bool Equals(object? obj)
        {
            if (!(obj is ArticleDto other))
            {
                return false;
            }

            return Title.Equals(other.Title) && NewsPortal.Id.Equals(other.NewsPortal.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NewsPortal.Id, Title);
        }

        public Article GetUpdatedEntity()
        {
            var articleCategories = Categories.Select(category =>
                new ArticleCategory(
                    id: new Guid(),
                    articleId: Id,
                    categoryId: category.Id,
                    article: null,
                    category: new Category(
                        id: category.Id,
                        name: category.Name,
                        color: category.Color,
                        keyWordsRegexPattern: category.KeyWordsRegexPattern,
                        sortIndex: category.SortIndex
                    )
                )).ToList();

            var newsPortal = new NewsPortal(
                id: NewsPortal.Id,
                name: NewsPortal.Name,
                baseUrl: NewsPortal.BaseUrl,
                iconUrl: NewsPortal.IconUrl,
                isNewOverride: NewsPortal.IsNewOverride,
                createdAt: NewsPortal.CreatedAt,
                categoryId: NewsPortal.CategoryId
            );

            return new Article(
                id: Id,
                articleId: ArticleId,
                url: Url,
                summary: Summary,
                title: Title,
                imageUrl: ImageUrl,
                createDateTime: CreateDateTime,
                updateDateTime: UpdateDateTime,
                publishDateTime: PublishDateTime,
                numberOfClicks: NumberOfClicks,
                trendingScore: TrendingScore,
                isHidden: IsHidden,
                newsPortalId: NewsPortal.Id,
                rssFeedId: RssFeedId,
                articleCategories: articleCategories,
                newsPortal: newsPortal,
                rssFeed: null
            );
        }

        public Article CreateEntity()
        {
            var articleCategories = Categories.Select(category =>
                new ArticleCategory(
                    id: new Guid(),
                    articleId: Id,
                    categoryId: category.Id,
                    article: null,
                    category: new Category(
                        id: category.Id,
                        name: category.Name,
                        color: category.Color,
                        keyWordsRegexPattern: category.KeyWordsRegexPattern,
                        sortIndex: category.SortIndex
                    )
                )).ToList();

            var newsPortal = new NewsPortal(
                id: NewsPortal.Id,
                name: NewsPortal.Name,
                baseUrl: NewsPortal.BaseUrl,
                iconUrl: NewsPortal.IconUrl,
                isNewOverride: NewsPortal.IsNewOverride,
                createdAt: NewsPortal.CreatedAt,
                categoryId: NewsPortal.CategoryId
            );

            return new Article(
                id: Id,
                articleId: ArticleId,
                url: Url,
                summary: Summary,
                title: Title,
                imageUrl: ImageUrl,
                createDateTime: CreateDateTime,
                updateDateTime: UpdateDateTime,
                publishDateTime: PublishDateTime,
                numberOfClicks: NumberOfClicks,
                trendingScore: TrendingScore,
                isHidden: IsHidden,
                newsPortalId: NewsPortal.Id,
                rssFeedId: RssFeedId,
                articleCategories: articleCategories,
                newsPortal: newsPortal,
                rssFeed: null
            );
        }
        #endregion
    }
}
