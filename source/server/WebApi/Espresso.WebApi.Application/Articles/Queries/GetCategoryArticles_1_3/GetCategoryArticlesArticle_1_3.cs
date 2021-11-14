// GetCategoryArticlesArticle_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3
{
#pragma warning disable S101 // Types should be named in PascalCase
    public record GetCategoryArticlesArticle_1_3
#pragma warning restore S101 // Types should be named in PascalCase
    {
        /// <summary>
        /// Gets iD created by app.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets article Url provided by RSS Feed.
        /// </summary>
        public string Url { get; private set; } = string.Empty;

        /// <summary>
        /// Gets article Title Parsed from RSS Feed.
        /// </summary>
        public string Title { get; private set; } = string.Empty;

        /// <summary>
        /// Gets image URL parsed from src attribute of first img element or second rss feed link, first is.
        /// </summary>
        public string? ImageUrl { get; private set; }

        /// <summary>
        /// Gets article Publish time provided by RSS Feed.
        /// </summary>
        public string PublishDateTime { get; private set; } = string.Empty;

        /// <summary>
        /// Gets news Portal ID.
        /// </summary>
        public GetCategoryArticlesNewsPortal_1_3? NewsPortal { get; private set; }

        /// <summary>
        /// Gets list Of Categories article belongs to.
        /// </summary>
        public IEnumerable<GetCategoryArticlesCategory_1_3> Categories { get; private set; } = new List<GetCategoryArticlesCategory_1_3>();

        private GetCategoryArticlesArticle_1_3()
        {
        }

        public static Expression<Func<Article, GetCategoryArticlesArticle_1_3>> GetProjection()
        {
            return article => new GetCategoryArticlesArticle_1_3
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.MobileAppDateTimeFormat),
                NewsPortal = GetCategoryArticlesNewsPortal_1_3.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .Select(articleCategory => articleCategory.Category)
                    .Select(GetCategoryArticlesCategory_1_3.GetProjection().Compile()!),
            };
        }
    }
}
