using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3
{
    public class GetConfigurationNewsPortal_1_3
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

        public static Expression<Func<NewsPortal, GetConfigurationNewsPortal_1_3>> GetProjection()
        {
            return newsPortal => new GetConfigurationNewsPortal_1_3
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
            };
        }
        #endregion

        #region Constructors
        private GetConfigurationNewsPortal_1_3()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        #endregion
    }
}
