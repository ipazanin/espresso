﻿// GetCategoryArticlesNewsPortal_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_1_3
{
    public record GetCategoryArticlesNewsPortal_1_3
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

        private GetCategoryArticlesNewsPortal_1_3()
        {
        }

        public static Expression<Func<NewsPortal, GetCategoryArticlesNewsPortal_1_3>> GetProjection()
        {
            return newsPortal => new GetCategoryArticlesNewsPortal_1_3
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
            };
        }
    }
}