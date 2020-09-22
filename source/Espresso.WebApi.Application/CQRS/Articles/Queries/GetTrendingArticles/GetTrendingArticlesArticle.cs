using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesArticle
    {
        #region Properties
        /// <summary>
        /// ID created by app
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Article Url provided by RSS Feed
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Article Title Parsed from RSS Feed
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Image URL parsed from src attribute of first img element or second rss feed link, first is 
        /// </summary>
        public string? ImageUrl { get; private set; }

        /// <summary>
        /// Article Publish time provided by RSS Feed
        /// </summary>
        public string PublishDateTime { get; private set; }

        /// <summary>
        /// Trending Score
        /// </summary>
        public int TrendingScore { get; private set; }

        /// <summary>
        /// News Portal
        /// </summary>
        public GetTrendingArticlesNewsPortal NewsPortal { get; private set; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<GetTrendingArticlesCategory> Categories { get; private set; } = new List<GetTrendingArticlesCategory>();
        #endregion

        #region Constructors

        private GetTrendingArticlesArticle()
        {
            Url = null!;
            Title = null!;
            PublishDateTime = null!;
            NewsPortal = null!;
        }

        #endregion

        #region Methods
        public static Expression<Func<Article, GetTrendingArticlesArticle>> GetProjection()
        {
            return article => new GetTrendingArticlesArticle
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.ArticleDateTimeFormat),
                NewsPortal = GetTrendingArticlesNewsPortal.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .Select(articleCategory => articleCategory.Category)
                    .Select(GetTrendingArticlesCategory.GetProjection().Compile()!),
                TrendingScore = (int)article.TrendingScore
            };
        }
        #endregion
    }
}
