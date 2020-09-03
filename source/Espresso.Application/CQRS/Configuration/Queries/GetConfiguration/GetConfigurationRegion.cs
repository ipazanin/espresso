using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationRegion
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Subtitle { get; private set; }

        public IEnumerable<GetConfigurationNewsPortal> NewsPortals { get; private set; } = new List<GetConfigurationNewsPortal>();
        #endregion

        #region Constructors
        private GetConfigurationRegion()
        {
            Name = null!;
            Subtitle = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Region, GetConfigurationRegion>> GetProjection(TimeSpan maxAgeOfNewNewsPortal)
        {
            return region => new GetConfigurationRegion
            {
                Id = region.Id,
                Name = region.Name,
                Subtitle = region.Subtitle,
                NewsPortals = region
                    .NewsPortals
                    .Select(
                        GetConfigurationNewsPortal
                            .GetProjection(maxAgeOfNewNewsPortal)
                            .Compile()
                    ),
            };
        }
        #endregion
    }
}
