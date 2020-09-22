using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.WebApi.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticlesQuery : Request<GetLatestArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; }

        public int Skip { get; }

        public Guid? FirstArticleId { get; }

        public IEnumerable<int>? NewsPortalIds { get; }

        public IEnumerable<int>? CategoryIds { get; }

        public int NewNewsPortalsPosition { get; }

        public string? TitleSearchQuery { get; }

        public TimeSpan MaxAgeOfNewNewsPortal { get; }
        #endregion

        #region Constructors
        public GetLatestArticlesQuery(
            int take,
            int skip,
            Guid? firstArticleId,
            string? newsPortalIdsString,
            string? categoryIdsString,
            int newNewsPortalsPosition,
            string? titleSearchQuery,
            TimeSpan maxAgeOfNewNewsPortal,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.GetLatestArticlesQuery
        )
        {
            Take = take;
            Skip = skip;
            FirstArticleId = firstArticleId;
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

            NewNewsPortalsPosition = newNewsPortalsPosition;
            TitleSearchQuery = titleSearchQuery;
            MaxAgeOfNewNewsPortal = maxAgeOfNewNewsPortal;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, " +
                $"{nameof(Skip)}:{Skip}, " +
                $"{nameof(FirstArticleId)}:{FirstArticleId}, " +
                $"{nameof(NewsPortalIds)}:{(NewsPortalIds is null ? "" : string.Join(",", NewsPortalIds))}, " +
                $"{nameof(CategoryIds)}:{(CategoryIds is null ? "" : string.Join(",", CategoryIds))}, " +
                $"{nameof(TitleSearchQuery)}:{TitleSearchQuery}";
        }
        #endregion
    }
}
