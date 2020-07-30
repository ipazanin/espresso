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
    public class ArticleTrendingViewModel
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
        public NewsPortalViewModel NewsPortal { get; private set; }

        /// <summary>
        /// List Of Categories article belongs to
        /// </summary>
        public IEnumerable<CategoryViewModel> Categories { get; private set; } = new List<CategoryViewModel>();
        #endregion

        #region Constructors

        private ArticleTrendingViewModel()
        {
            Url = null!;
            Title = null!;
            PublishDateTime = null!;
            NewsPortal = null!;
        }

        #endregion

        #region Methods
        public static Expression<Func<Article, ArticleTrendingViewModel>> GetProjection()
        {
            return article => new ArticleTrendingViewModel
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
                    .Select(articleCategory => articleCategory.Category)
                    .Select(CategoryViewModel.GetProjection().Compile()!),
                TrendingScore = (int)article.TrendingScore
            };
        }
        #endregion
    }
}
