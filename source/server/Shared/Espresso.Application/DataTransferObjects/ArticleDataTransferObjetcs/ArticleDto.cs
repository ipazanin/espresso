// ArticleDto.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects
{
    public record ArticleDto
    {
        /// <summary>
        /// Gets article id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets article url.
        /// </summary>
        public string Url { get; private set; } = string.Empty;

        /// <summary>
        /// Gets article web url.
        /// </summary>
        /// <remarks>
        /// Web url might differ from url because url might be AMP url.
        /// </remarks>
        public string WebUrl { get; private set; } = string.Empty;

        /// <summary>
        /// Gets article summary.
        /// </summary>
        public string Summary { get; private set; } = string.Empty;

        /// <summary>
        /// Gets article title.
        /// </summary>
        public string Title { get; private set; } = string.Empty;

        /// <summary>
        /// gets article image url.
        /// </summary>
        public string? ImageUrl { get; private set; }

        /// <summary>
        /// Gets date time when article was created in aplication.
        /// </summary>
        public DateTime CreateDateTime { get; private set; }

        /// <summary>
        /// Gets date time when article was updated in application.
        /// </summary>
        public DateTime UpdateDateTime { get; private set; }

        /// <summary>
        /// Gets date time when article was published.
        /// </summary>
        public DateTime PublishDateTime { get; private set; }

        /// <summary>
        /// Gets number of clicks on this article in application.
        /// </summary>
        public int NumberOfClicks { get; private set; }

        /// <summary>
        /// Gets calculated trending score based on age and number of clicks.
        /// </summary>
        public decimal TrendingScore { get; private set; }

        /// <summary>
        /// Gets associated news portal id.
        /// </summary>
        public int NewsPortalId { get; private set; }

        /// <summary>
        /// Gets associated rss feed id.
        /// </summary>
        public int RssFeedId { get; private set; }

        /// <summary>
        /// Gets article categories.
        /// </summary>
        public IEnumerable<ArticleCategoryDto> ArticleCategories { get; private set; } = new List<ArticleCategoryDto>();

        /// <summary>
        /// Gets main similar article.
        /// </summary>
        public SimilarArticleDto? MainArticle { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleDto"/> class.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="url">Url.</param>
        /// <param name="webUrl">Web url.</param>
        /// <param name="summary">Summary.</param>
        /// <param name="title">Title.</param>
        /// <param name="imageUrl">Image Url.</param>
        /// <param name="createDateTime">Create date time.</param>
        /// <param name="updateDateTime">Update date time.</param>
        /// <param name="publishDateTime">Publish date time.</param>
        /// <param name="numberOfClicks">Number of clicks.</param>
        /// <param name="trendingScore">Trending score.</param>
        /// <param name="newsPortalId">News portal id.</param>
        /// <param name="rssFeedId">Rss feed id.</param>
        /// <param name="articleCategories">Article categories.</param>
        /// <param name="mainArticle">Main similar article.</param>
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
            SimilarArticleDto? mainArticle)
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

        /// <summary>
        /// Creates <see cref="Article"/> to <see cref="ArticleDto"/> projection.
        /// </summary>
        /// <returns><see cref="Article"/> to <see cref="ArticleDto"/> projection.</returns>
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
    }
}
