﻿using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
    public record GetLatestArticlesNewsPortal_1_4
    {
        #region Properties
        /// <summary>
        /// News Portal ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// News Portal Name
        /// </summary>
        public string Name { get; private set; } = "";

        public string IconUrl { get; private set; } = "";
        #endregion

        #region Constructors
        private GetLatestArticlesNewsPortal_1_4()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<NewsPortal, GetLatestArticlesNewsPortal_1_4>> GetProjection()
        {
            return newsPortal => new GetLatestArticlesNewsPortal_1_4
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl
            };
        }
        #endregion
    }
}