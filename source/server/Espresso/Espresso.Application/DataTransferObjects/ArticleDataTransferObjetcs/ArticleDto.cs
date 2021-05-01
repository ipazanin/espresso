using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects
{
    public record ArticleDto
    {
        #region Properties
        public Guid Id { get; private set; }

        public string Url { get; private set; } = string.Empty;

        public string WebUrl { get; private set; } = string.Empty;

        public string Summary { get; private set; } = string.Empty;

        public string Title { get; private set; } = string.Empty;

        public string? ImageUrl { get; private set; }

        public DateTime CreateDateTime { get; private set; }

        public DateTime UpdateDateTime { get; private set; }

        public DateTime PublishDateTime { get; private set; }

        public int NumberOfClicks { get; private set; }

        public decimal TrendingScore { get; private set; }

        public int NewsPortalId { get; private set; }

        public int RssFeedId { get; private set; }

        public IEnumerable<ArticleCategoryDto> ArticleCategories { get; private set; } = new List<ArticleCategoryDto>();

        public SimilarArticleDto? MainArticle { get; private set; }
        #endregion

        #region Constructors
        [JsonConstructor]
        public ArticleDto(
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
            int newsPortalId,
            int rssFeedId,
            IEnumerable<ArticleCategoryDto> articleCategories,
            SimilarArticleDto? mainArticle
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
            NewsPortalId = newsPortalId;
            RssFeedId = rssFeedId;
            ArticleCategories = articleCategories;
            MainArticle = mainArticle;
        }

        private ArticleDto()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<Article, ArticleDto>> GetProjection()
        {
            var articleCategoryDtoProjection = ArticleCategoryDto.GetProjection().Compile();
            var similarArticleDtoProjection = SimilarArticleDto.GetProjection().Compile();
            return article => new ArticleDto
            {
                Id = article.Id,
                Url = article.Url,
                WebUrl = article.WebUrl,
                Summary = article.Summary,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                CreateDateTime = article.CreateDateTime,
                UpdateDateTime = article.UpdateDateTime,
                PublishDateTime = article.PublishDateTime,
                NumberOfClicks = article.NumberOfClicks,
                TrendingScore = article.TrendingScore,
                NewsPortalId = article.NewsPortalId,
                RssFeedId = article.RssFeedId,
                ArticleCategories = article.ArticleCategories.Select(articleCategoryDtoProjection),
                MainArticle = article.MainArticle == null ?
                    null :
                    similarArticleDtoProjection.Invoke(article.MainArticle),
            };
        }
        #endregion
    }
}
