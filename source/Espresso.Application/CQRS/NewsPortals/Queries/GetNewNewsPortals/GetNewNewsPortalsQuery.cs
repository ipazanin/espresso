using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewNewsportals
{
    public class GetNewNewsportalsQuery : Request<GetNewNewsPortalsQueryResponse>
    {
        #region Properties
        public IEnumerable<int>? NewsPortalIds { get; }

        public IEnumerable<int>? CategoryIds { get; }
        #endregion

        #region Constructors
        public GetNewNewsportalsQuery(
            string? newsPortalIdsString,
            string? categoryIdsString,
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.GetNewNewsPortalsQuery
        )
        {
            var newsPortalIds = newsPortalIdsString
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            NewsPortalIds = newsPortalIds is null || newsPortalIds.Any() ? newsPortalIds : null;

            var categoryIds = categoryIdsString
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(categoryIdString => int.TryParse(categoryIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            CategoryIds = categoryIds is null || categoryIds.Any() ? categoryIds : null;

        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(NewsPortalIds)}:{(NewsPortalIds is null ? "" : string.Join(",", NewsPortalIds))}, " +
                $"{nameof(CategoryIds)}:{(CategoryIds is null ? "" : string.Join(",", CategoryIds))}";
        }
        #endregion

    }
}
