using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public record GetFeaturedArticlesArticle
    {
        #region Properties
        /// <summary>
        /// ID created by app
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Article Url provided by RSS Feed
        /// </summary>
        public string Url { get; init; } = "";

        /// <summary>
        /// Article Url provided by RSS Feed
        /// </summary>
        public string WebUrl { get; init; } = "";

        /// <summary>
        /// Article Title Parsed from RSS Feed
        /// </summary>
        public string Title { get; init; } = "";

        /// <summary>
        /// Image URL parsed from src attribute of first img element or second rss feed link, first is 
        /// </summary>
        public string? ImageUrl { get; init; }

        /// <summary>
        /// Article Publish time provided by RSS Feed
        /// </summary>
        public string PublishDateTime { get; init; } = "";

        /// <summary>
        /// News Portal
        /// </summary>
        public GetFeaturedArticlesNewsPortal? NewsPortal { get; init; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<GetFeaturedArticlesCategory> Categories { get; init; } = new List<GetFeaturedArticlesCategory>();
        #endregion

        #region Methods
        public static Expression<Func<Article, GetFeaturedArticlesArticle>> GetProjection()
        {
            return article => new GetFeaturedArticlesArticle
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                WebUrl = article.WebUrl,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.ArticleDateTimeFormat),
                NewsPortal = GetFeaturedArticlesNewsPortal.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .Select(articleCategory => articleCategory.Category)
                    .Select(GetFeaturedArticlesCategory.GetProjection().Compile()!)
            };
        }
        #endregion
    }
}
