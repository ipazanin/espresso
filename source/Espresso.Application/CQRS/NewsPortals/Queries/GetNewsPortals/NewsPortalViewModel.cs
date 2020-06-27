﻿using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals
{
    public class NewsPortalViewModel
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

        public static Expression<Func<NewsPortal, NewsPortalViewModel>> Projection => newsPortal => new NewsPortalViewModel
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl
        };
        #endregion

        #region Constructors
        private NewsPortalViewModel()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        #endregion
    }
}
