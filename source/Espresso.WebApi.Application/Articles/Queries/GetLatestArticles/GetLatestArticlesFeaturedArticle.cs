using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles
{
    public record GetLatestArticlesFeaturedArticle
    {
        #region Properties
        /// <summary>
        /// ID created by app
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Article Url provided by RSS Feed
        /// </summary>
        public string Url { get; private set; } = "";

        /// <summary>
        /// Article Url provided by RSS Feed
        /// </summary>
        public string WebUrl { get; private set; } = "";

        /// <summary>
        /// Article Title Parsed from RSS Feed
        /// </summary>
        public string Title { get; private set; } = "";

        /// <summary>
        /// Image URL parsed from src attribute of first img element or second rss feed link, first is 
        /// </summary>
        public string? ImageUrl { get; private set; }

        /// <summary>
        /// Article Publish time provided by RSS Feed
        /// </summary>
        public string PublishDateTime { get; private set; } = "";

        /// <summary>
        /// News Portal ID
        /// </summary>
        public GetLatestArticlesNewsPortal? NewsPortal { get; private set; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<GetLatestArticlesCategory> Categories { get; private set; } = new List<GetLatestArticlesCategory>();
        #endregion

        #region Constructors
        private GetLatestArticlesFeaturedArticle()
        {

        }
        #endregion

        #region Methods
        public static Expression<Func<Article, GetLatestArticlesFeaturedArticle>> GetProjection()
        {
            return article => new GetLatestArticlesFeaturedArticle
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                WebUrl = article.WebUrl,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.ArticleDateTimeFormat),
                NewsPortal = GetLatestArticlesNewsPortal.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .AsQueryable()
                    .Select(articleCategory => articleCategory.Category)
                    .Select(GetLatestArticlesCategory.GetProjection()!)
            };
        }
        #endregion
    }
}
