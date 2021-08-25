﻿// GetFeaturedArticlesArticle.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public record GetFeaturedArticlesArticle
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
        /// Gets article Url provided by RSS Feed.
        /// </summary>
        public string WebUrl { get; private set; } = string.Empty;

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
        /// 
        /// </summary>
        /// <value></value>
        public int NumberOfClicks { get; private set; }

        /// <summary>
        /// Gets news Portal.
        /// </summary>
        public GetFeaturedArticlesNewsPortal? NewsPortal { get; private set; }

        /// <summary>
        /// Gets list Of Categories article belongs to.
        /// </summary>
        public IEnumerable<GetFeaturedArticlesCategory> Categories { get; private set; } = new List<GetFeaturedArticlesCategory>();

        private GetFeaturedArticlesArticle()
        {
        }

        public static Expression<Func<Article, GetFeaturedArticlesArticle>> GetProjection()
        {
            return article => new GetFeaturedArticlesArticle
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                WebUrl = article.WebUrl,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.MobileAppDateTimeFormat),
                NumberOfClicks = article.NumberOfClicks,
                NewsPortal = GetFeaturedArticlesNewsPortal.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .Select(articleCategory => articleCategory.Category)
                    .Select(GetFeaturedArticlesCategory.GetProjection().Compile()!),
            };
        }
    }
}
