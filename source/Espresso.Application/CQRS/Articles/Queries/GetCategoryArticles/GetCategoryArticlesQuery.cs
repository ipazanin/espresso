using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQuery : Request<GetCategoryArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; }

        public int Skip { get; }

        public int CategoryId { get; }

        public int? RegionId { get; }

        public IEnumerable<int>? NewsPortalIds { get; }

        public int NewNewsPortalsPosition { get; }

        public string? TitleSearchQuery { get; }
        #endregion

        #region Constructors
        public GetCategoryArticlesQuery(
            int? take,
            int? skip,
            int categoryId,
            string? newsPortalIdsString,
            int? regionId,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            int newNewsPortalsPosition,
            string? titleSearchQuery
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            Event.GetCategoryArticlesQuery
        )
        {
            Take = take ?? DefaultValueConstants.DefaultTake;
            Skip = skip ?? DefaultValueConstants.DefaultSkip;
            CategoryId = categoryId;
            RegionId = regionId;
            var newsPortalIds = newsPortalIdsString
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            NewsPortalIds = newsPortalIds == null || newsPortalIds.Any() ? newsPortalIds : null;
            NewNewsPortalsPosition = newNewsPortalsPosition;
            TitleSearchQuery = titleSearchQuery;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, {nameof(Skip)}:{Skip}, {nameof(CategoryId)}:{CategoryId}, " +
                $"{nameof(NewsPortalIds)}:{(NewsPortalIds is null ? "" : string.Join(",", NewsPortalIds))}";
        }
        #endregion
    }
}
