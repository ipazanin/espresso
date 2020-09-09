using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQuery : Request<GetCategoryArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; }

        public int Skip { get; }

        public Guid? FirstArticleId { get; }

        public int CategoryId { get; }

        public int? RegionId { get; }

        public IEnumerable<int>? NewsPortalIds { get; }

        public int NewNewsPortalsPosition { get; }

        public string? TitleSearchQuery { get; }

        public TimeSpan MaxAgeOfNewNewsPortal { get; }
        #endregion

        #region Constructors
        public GetCategoryArticlesQuery(
            int take,
            int skip,
            Guid? firstArticleId,
            int categoryId,
            string? newsPortalIdsString,
            int? regionId,
            TimeSpan maxAgeOfNewNewsPortal,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment,
            int newNewsPortalsPosition,
            string? titleSearchQuery
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.GetCategoryArticlesQuery
        )
        {
            Take = take;
            Skip = skip;
            FirstArticleId = firstArticleId;
            CategoryId = categoryId;
            RegionId = regionId;
            MaxAgeOfNewNewsPortal = maxAgeOfNewNewsPortal;
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
            return $"{nameof(Take)}:{Take}, " +
                $"{nameof(Skip)}:{Skip}, " +
                $"{nameof(FirstArticleId)}:{FirstArticleId}, " +
                $"{nameof(CategoryId)}:{CategoryId}, " +
                $"{nameof(NewsPortalIds)}:{(NewsPortalIds is null ? "" : string.Join(",", NewsPortalIds))}";
        }
        #endregion
    }
}
