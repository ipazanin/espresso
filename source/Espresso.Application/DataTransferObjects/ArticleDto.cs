using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects
{
    public record ArticleDto
    {
        #region Properties

        /// <summary>
        /// ID created by app
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Article Url provided by RSS Feed or AMP Url
        /// </summary>
        public string Url { get; init; }


        /// <summary>
        /// Article Url provided by RSS Feed
        /// </summary>
        public string WebUrl { get; init; }

        /// <summary>
        /// Article Title Parsed from RSS Feed
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Image URL parsed from src attribute of first img element or second rss feed link, first is 
        /// </summary>
        public string? ImageUrl { get; init; }

        /// <summary>
        /// Article Publish time provided by RSS Feed
        /// </summary>
        public DateTime PublishDateTime { get; init; }

        public DateTime CreateDateTime { get; init; }

        public DateTime UpdateDateTime { get; init; }

        public decimal TrendingScore { get; init; }

        public int NumberOfClicks { get; init; }

        public string Summary { get; init; }

        public int RssFeedId { get; init; }

        /// <summary>
        /// News Portal ID
        /// </summary>
        public NewsPortalDto NewsPortal { get; init; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<CategoryDto> Categories { get; init; } = new List<CategoryDto>();

        #endregion

        #region Constructors
        /// <summary>
        /// Used by JSON serializer
        /// </summary>
        // [JsonConstructor]
        public ArticleDto()
        {
            Url = null!;
            WebUrl = null!;
            Title = null!;
            Summary = null!;
            NewsPortal = null!;
        }

        #endregion

        #region Methods
        public static Expression<Func<Article, ArticleDto>> GetProjection()
        {
            return article => new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                WebUrl = article.WebUrl,
                PublishDateTime = article.PublishDateTime,
                CreateDateTime = article.CreateDateTime,
                UpdateDateTime = article.UpdateDateTime,
                NewsPortal = NewsPortalDto.GetProjection().Compile().Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .Select(articleCategory => articleCategory.Category)
                    .Select(CategoryDto.GetProjection().Compile()!),
                TrendingScore = article.TrendingScore,
                NumberOfClicks = article.NumberOfClicks,
                Summary = article.Summary,
                RssFeedId = article.RssFeedId,
            };
        }

        public static Func<ArticleDto, Article> GetToArticleProjection()
        {
            return article =>
            {
                var articleCategories = article.Categories.Select(
                    category => new ArticleCategory(
                        id: new Guid(),
                        articleId: article.Id,
                        categoryId: category.Id,
                        article: null,
                        category: new Category(
                            id: category.Id,
                            name: category.Name,
                            color: category.Color,
                            keyWordsRegexPattern: category.KeyWordsRegexPattern,
                            sortIndex: category.SortIndex,
                            position: category.Position,
                            categoryType: category.CategoryType,
                            categoryUrl: category.Url
                        )
                    )
                ).ToList();

                var newsPortal = new NewsPortal(
                    id: article.NewsPortal.Id,
                    name: article.NewsPortal.Name,
                    baseUrl: article.NewsPortal.BaseUrl,
                    iconUrl: article.NewsPortal.IconUrl,
                    isNewOverride: article.NewsPortal.IsNewOverride,
                    createdAt: article.NewsPortal.CreatedAt,
                    categoryId: article.NewsPortal.CategoryId,
                    regionId: article.NewsPortal.RegionId
                );

                var createdArticle = new Article(
                    id: article.Id,
                    url: article.Url,
                    webUrl: article.WebUrl,
                    summary: article.Summary,
                    title: article.Title,
                    imageUrl: article.ImageUrl,
                    createDateTime: article.CreateDateTime,
                    updateDateTime: article.UpdateDateTime,
                    publishDateTime: article.PublishDateTime,
                    numberOfClicks: article.NumberOfClicks,
                    trendingScore: article.TrendingScore,
                    isHidden: Article.IsHiddenDefaultValue,
                    isFeatured: Article.IsFeaturedDefaultValue,
                    newsPortalId: article.NewsPortal.Id,
                    rssFeedId: article.RssFeedId,
                    articleCategories: articleCategories,
                    newsPortal: newsPortal,
                    rssFeed: null
                );
                return createdArticle;
            };
        }
        #endregion
    }
}
