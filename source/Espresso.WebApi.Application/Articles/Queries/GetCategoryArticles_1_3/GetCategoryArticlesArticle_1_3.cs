using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3
{
    public class GetCategoryArticlesArticle_1_3
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
        /// News Portal ID
        /// </summary>
        public GetCategoryArticlesNewsPortal_1_3 NewsPortal { get; private set; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<GetCategoryArticlesCategory_1_3> Categories { get; private set; } = new List<GetCategoryArticlesCategory_1_3>();

        #endregion

        #region Constructors
        private GetCategoryArticlesArticle_1_3()
        {
            Url = null!;
            Title = null!;
            PublishDateTime = null!;
            NewsPortal = null!;
        }

        public GetCategoryArticlesArticle_1_3(
            Guid id,
            string url,
            string title,
            string? imageUrl,
            string publishDateTime,
            GetCategoryArticlesNewsPortal_1_3 newsPortal,
            IEnumerable<GetCategoryArticlesCategory_1_3> categories
        )
        {
            Id = id;
            Url = url;
            Title = title;
            ImageUrl = imageUrl;
            PublishDateTime = publishDateTime;
            NewsPortal = newsPortal;
            Categories = categories;
        }
        #endregion

        #region Methods
        public static Expression<Func<Article, GetCategoryArticlesArticle_1_3>> GetProjection()
        {
            return article => new GetCategoryArticlesArticle_1_3
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.ArticleDateTimeFormat),
                NewsPortal = GetCategoryArticlesNewsPortal_1_3.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .Select(articleCategory => articleCategory.Category)
                    .Select(GetCategoryArticlesCategory_1_3.GetProjection().Compile()!)
            };
        }
        #endregion
    }
}
