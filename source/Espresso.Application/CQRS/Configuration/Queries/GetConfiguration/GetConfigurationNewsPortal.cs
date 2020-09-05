using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationNewsPortal
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

        public bool IsNew { get; private set; }

        public int CategoryId { get; private set; }

        public int RegionId { get; private set; }

        public static Expression<Func<NewsPortal, GetConfigurationNewsPortal>> GetProjection(TimeSpan maxAgeOfNewNewsPortal)
        {
            var newNewsPortalMinDate = DateTime.UtcNow - maxAgeOfNewNewsPortal;
            return newsPortal => new GetConfigurationNewsPortal
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
                IsNew = newsPortal.IsNewOverride != null ?
                    newsPortal.IsNewOverride.Value :
                    newsPortal.CreatedAt > newNewsPortalMinDate,
                CategoryId = newsPortal.CategoryId,
                RegionId = newsPortal.RegionId
            };
        }
        #endregion

        #region Constructors
        private GetConfigurationNewsPortal()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        #endregion
    }
}
