using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals
{
    public class GetNewsPortalsNewsPortal
    {

        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string IconUrl { get; private set; }

        public string BaseUrl { get; private set; }

        public GetNewsPortalsCategory Category { get; private set; }
        #endregion Properties

        #region Constructors
        private GetNewsPortalsNewsPortal()
        {
            Name = null!;
            IconUrl = null!;
            BaseUrl = null!;
            Category = null!;
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<NewsPortal, GetNewsPortalsNewsPortal>> GetProjection()
        {
            var categoryProjection = GetNewsPortalsCategory.GetProjection().Compile();
            return newsPortal => new GetNewsPortalsNewsPortal
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
                BaseUrl = newsPortal.BaseUrl,
                Category = categoryProjection.Invoke(newsPortal.Category!)
            };
        }
        #endregion Methods
    }
}
