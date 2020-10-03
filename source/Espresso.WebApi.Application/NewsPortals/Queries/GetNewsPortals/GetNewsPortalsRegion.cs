using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.NewsPortals
{
    public class GetNewsPortalsRegion
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; } = "";

        public string Subtitle { get; private set; } = "";
        #endregion

        #region Constructors
        private GetNewsPortalsRegion()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<Region, GetNewsPortalsRegion>> GetProjection()
        {
            return region => new GetNewsPortalsRegion
            {
                Id = region.Id,
                Name = region.Name,
                Subtitle = region.Subtitle,
            };
        }
        #endregion
    }
}