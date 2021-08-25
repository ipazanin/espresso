// GetLatestArticlesNewsPortal_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
    public record GetLatestArticlesNewsPortal_1_3
    {
        /// <summary>
        /// Gets news Portal ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets news Portal Name.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        public string IconUrl { get; private set; } = string.Empty;

        private GetLatestArticlesNewsPortal_1_3()
        {
        }

        public static Expression<Func<NewsPortal, GetLatestArticlesNewsPortal_1_3>> GetProjection()
        {
            return newsPortal => new GetLatestArticlesNewsPortal_1_3
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
            };
        }
    }
}
