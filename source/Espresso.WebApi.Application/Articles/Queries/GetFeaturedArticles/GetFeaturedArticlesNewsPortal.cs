using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public record GetFeaturedArticlesNewsPortal
    {
        #region Properties
        /// <summary>
        /// News Portal ID
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// News Portal Name
        /// </summary>
        public string Name { get; init; } = "";

        public string IconUrl { get; init; } = "";
        #endregion

        #region Methods
        public static Expression<Func<NewsPortal, GetFeaturedArticlesNewsPortal>> GetProjection()
        {
            return newsPortal => new GetFeaturedArticlesNewsPortal
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl
            };
        }
        #endregion
    }
}
