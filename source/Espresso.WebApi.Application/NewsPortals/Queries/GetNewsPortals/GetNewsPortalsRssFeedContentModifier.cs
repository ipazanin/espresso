using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.NewsPortals
{
    public class GetNewsPortalsRssFeedContentModifier
    {

        #region Properties
        public int Id { get; private set; }

        public string SourceValue { get; private set; } = "";

        public string ReplacementValue { get; private set; } = "";
        #endregion

        #region Constructors
        private GetNewsPortalsRssFeedContentModifier()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<RssFeedContentModifier, GetNewsPortalsRssFeedContentModifier>> GetProjection()
        {
            return rssFeedCategory => new GetNewsPortalsRssFeedContentModifier
            {
                Id = rssFeedCategory.Id,
                SourceValue = rssFeedCategory.SourceValue,
                ReplacementValue = rssFeedCategory.ReplacementValue,
            };
        }
        #endregion
    }
}