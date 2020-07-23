using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.ViewModels.NewsPortalViewModels
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
        #endregion

        #region Constructors
        private NewsPortalViewModel()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<NewsPortal, NewsPortalViewModel>> GetProjection()
        {
            return newsPortal => new NewsPortalViewModel
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl
            };
        }
        #endregion
    }
}
