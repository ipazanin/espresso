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

        public string Summary { get; set; }

        public int RssFeedId { get; set; }

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
            string summary,
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
            Summary = summary;
            RssFeedId = rssFeedId;
            NewsPortal = newsPortal;
            Categories = categories;
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

        public override bool Equals(object? obj)
        {
            return obj is ArticleDto other &&
            Title.Equals(other.Title) &&
            NewsPortal.Id.Equals(other.NewsPortal.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NewsPortal.Id, Title);
        }
        #endregion
    }
}
