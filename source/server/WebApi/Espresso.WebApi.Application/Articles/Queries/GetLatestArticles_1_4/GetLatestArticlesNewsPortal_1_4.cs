// GetLatestArticlesNewsPortal_1_4.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
    public record GetLatestArticlesNewsPortal_1_4
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

        private GetLatestArticlesNewsPortal_1_4()
        {
        }

        public static Expression<Func<NewsPortal, GetLatestArticlesNewsPortal_1_4>> GetProjection()
        {
            return newsPortal => new GetLatestArticlesNewsPortal_1_4
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
            };
        }
    }
}
