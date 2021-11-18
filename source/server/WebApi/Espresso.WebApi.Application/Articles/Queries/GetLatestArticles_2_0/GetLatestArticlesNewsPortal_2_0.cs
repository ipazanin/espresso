// GetLatestArticlesNewsPortal_2_0.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_2_0
{
#pragma warning disable S101 // Types should be named in PascalCase
    public record GetLatestArticlesNewsPortal_2_0
#pragma warning restore S101 // Types should be named in PascalCase
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

        private GetLatestArticlesNewsPortal_2_0()
        {
        }

        public static Expression<Func<NewsPortal, GetLatestArticlesNewsPortal_2_0>> GetProjection()
        {
            return newsPortal => new GetLatestArticlesNewsPortal_2_0
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
            };
        }
    }
}
