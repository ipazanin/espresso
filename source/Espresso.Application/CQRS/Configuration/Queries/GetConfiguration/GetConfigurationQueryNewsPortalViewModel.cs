using System;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryNewsPortalViewModel
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

        public static Expression<Func<NewsPortal, GetConfigurationQueryNewsPortalViewModel>> GetProjection()
        {
            return newsPortal => new GetConfigurationQueryNewsPortalViewModel
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
                IsNew = newsPortal.IsNewOverride != null ?
                    newsPortal.IsNewOverride.Value :
                    (newsPortal.CreatedAt > (DateTime.UtcNow - DateTimeConstants.MaxAgeOfNewNewsPortal)),
                CategoryId = newsPortal.CategoryId,
            };
        }
        #endregion

        #region Constructors
        private GetConfigurationQueryNewsPortalViewModel()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        #endregion
    }
}
