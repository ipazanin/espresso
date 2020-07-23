using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.ViewModels.NewsPortalViewModels
{
    public class NewsPortalArticleItem
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string IconUrl { get; private set; }
        #endregion

        #region Constructors
        private NewsPortalArticleItem()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<NewsPortal, NewsPortalArticleItem>> GetProjection()
        {
            return newsPortal => new NewsPortalArticleItem
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl
            };
        }
        #endregion
    }
}
