﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0
{
    public record GetLatestArticlesArticle_2_0
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
        /// 
        /// </summary>
        /// <value></value>
        public int NumberOfClicks { get; private set; }

        /// <summary>
        /// News Portal ID
        /// </summary>
        public GetLatestArticlesNewsPortal_2_0? NewsPortal { get; private set; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<GetLatestArticlesCategory_2_0> Categories { get; private set; } = new List<GetLatestArticlesCategory_2_0>();
        #endregion

        #region Constructors
        private GetLatestArticlesArticle_2_0()
        {

        }
        #endregion

        #region Methods
        public static Expression<Func<Article, GetLatestArticlesArticle_2_0>> GetProjection()
        {
            return article => new GetLatestArticlesArticle_2_0
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                WebUrl = article.WebUrl,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.MobileAppDateTimeFormat),
                NumberOfClicks = article.NumberOfClicks,
                NewsPortal = GetLatestArticlesNewsPortal_2_0.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .AsQueryable()
                    .Select(articleCategory => articleCategory.Category)
                    .Select(GetLatestArticlesCategory_2_0.GetProjection()!)
            };
        }
        #endregion
    }
}