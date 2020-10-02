using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
    public record GetLatestArticlesNewsPortal_1_3
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
        public static Expression<Func<NewsPortal, GetLatestArticlesNewsPortal_1_3>> GetProjection()
        {
            return newsPortal => new GetLatestArticlesNewsPortal_1_3
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl
            };
        }
        #endregion
    }
}
