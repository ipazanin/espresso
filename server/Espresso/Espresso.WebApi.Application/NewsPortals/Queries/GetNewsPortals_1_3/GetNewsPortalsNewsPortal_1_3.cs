using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals_1_3
{
    public class GetNewsPortalsNewsPortal_1_3
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
        private GetNewsPortalsNewsPortal_1_3()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<NewsPortal, GetNewsPortalsNewsPortal_1_3>> GetProjection()
        {
            return newsPortal => new GetNewsPortalsNewsPortal_1_3
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl
            };
        }
        #endregion
    }
}
