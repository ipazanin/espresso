using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Common.Enums;

namespace Espresso.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticlesQuery : Request<GetLatestArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; }

        public int Skip { get; }

        public IEnumerable<int>? NewsPortalIds { get; }

        public IEnumerable<int>? CategoryIds { get; }
        #endregion

        #region Constructors
        public GetLatestArticlesQuery(
            int? take,
            int? skip,
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
            Event.GetLatestArticlesQuery
        )
        {
            Take = take ?? DefaultValueConstants.DefaultTake;
            Skip = skip ?? DefaultValueConstants.DefaultSkip;

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
                ?.Where(categoryId => categoryId != default);

            CategoryIds = categoryIds == null || categoryIds.Any() ? categoryIds : null;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, {nameof(Skip)}:{Skip}, " +
            $"{nameof(NewsPortalIds)}:{(NewsPortalIds is null ? "" : string.Join(",", NewsPortalIds))}, " +
            $"{nameof(CategoryIds)}:{(CategoryIds is null ? "" : string.Join(",", CategoryIds))}";
        }
        #endregion
    }
}
