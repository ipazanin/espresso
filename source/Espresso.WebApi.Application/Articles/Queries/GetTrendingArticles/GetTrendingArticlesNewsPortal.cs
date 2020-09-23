using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesNewsPortal
    {
        #region Properties
        /// <summary>
        /// News Portal ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// News Portal Name
        /// </summary>
        public string Name { get; private set; }

        public string IconUrl { get; private set; }
        #endregion

        #region Constructors
        private GetTrendingArticlesNewsPortal()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<NewsPortal, GetTrendingArticlesNewsPortal>> GetProjection()
        {
            return newsPortal => new GetTrendingArticlesNewsPortal
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl
            };
        }
        #endregion
    }
}
