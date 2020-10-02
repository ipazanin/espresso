using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
    public record GetLatestArticlesArticle_1_3
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
        /// News Portal ID
        /// </summary>
        public GetLatestArticlesNewsPortal_1_3? NewsPortal { get; init; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<GetLatestArticlesCategory_1_3> Categories { get; init; } = new List<GetLatestArticlesCategory_1_3>();

        #endregion

        #region Methods
        public static Expression<Func<Article, GetLatestArticlesArticle_1_3>> GetProjection()
        {
            return article => new GetLatestArticlesArticle_1_3
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.ArticleDateTimeFormat),
                NewsPortal = GetLatestArticlesNewsPortal_1_3.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .AsQueryable()
                    .Select(articleCategory => articleCategory.Category)
                    .Select(GetLatestArticlesCategory_1_3.GetProjection()!)
            };
        }
        #endregion
    }
}
