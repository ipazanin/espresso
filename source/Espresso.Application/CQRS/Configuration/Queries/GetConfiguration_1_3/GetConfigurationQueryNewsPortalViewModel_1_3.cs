using System;
using System.Linq.Expressions;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration_1_3
{
    public class GetConfigurationQueryNewsPortalViewModel_1_3
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

        public static Expression<Func<NewsPortal, GetConfigurationQueryNewsPortalViewModel_1_3>> Projection => newsPortal => new GetConfigurationQueryNewsPortalViewModel_1_3
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl,
        };
        #endregion

        #region Constructors
        private GetConfigurationQueryNewsPortalViewModel_1_3()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        #endregion
    }
}
