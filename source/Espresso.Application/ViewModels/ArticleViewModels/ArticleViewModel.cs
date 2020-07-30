using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Application.ViewModels.CategoryViewModels;
using Espresso.Application.ViewModels.NewsPortalViewModels;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.Application.ViewModels.ArticleViewModels
{
    public class ArticleViewModel
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
        public NewsPortalViewModel NewsPortal { get; private set; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<CategoryViewModel> Categories { get; private set; } = new List<CategoryViewModel>();

        #endregion

        #region Constructors
        private ArticleViewModel()
        {
            Url = null!;
            Title = null!;
            PublishDateTime = null!;
            NewsPortal = null!;
        }

        public ArticleViewModel(
            Guid id,
            string url,
            string title,
            string? imageUrl,
            string publishDateTime,
            NewsPortalViewModel newsPortal,
            IEnumerable<CategoryViewModel> categories
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
        public static Expression<Func<Article, ArticleViewModel>> GetProjection()
        {
            return article => new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                ImageUrl = article.ImageUrl,
                Url = article.Url,
                PublishDateTime = article.PublishDateTime.ToString(DateTimeConstants.MobileAppDateTimeFormat),
                NewsPortal = NewsPortalViewModel.GetProjection()
                    .Compile()
                    .Invoke(article.NewsPortal!),
                Categories = article.ArticleCategories
                    .AsQueryable()
                    .Select(articleCategory => articleCategory.Category)
                    .Select(CategoryViewModel.GetProjection()!)
            };
        }
        #endregion
    }
}
