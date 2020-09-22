using System.Collections.Generic;
using System.Linq;
using Espresso.WebApi.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetLatestArticles_1_3
{
    public class GetLatestArticlesQuery_1_3 : Request<GetLatestArticlesQueryResponse_1_3>
    {
        #region Properties
        public int Take { get; }

        public int Skip { get; }

        public IEnumerable<int>? NewsPortalIds { get; }

        public IEnumerable<int>? CategoryIds { get; }

        public string? TitleSearchQuery { get; }
        #endregion

        #region Constructors
        public GetLatestArticlesQuery_1_3(
            int? take,
            int? skip,
            string? newsPortalIdsString,
            string? categoryIdsString,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment,
            string? titleSearchQuery
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
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

            TitleSearchQuery = titleSearchQuery;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, {nameof(Skip)}:{Skip}, " +
            $"{nameof(NewsPortalIds)}:{(NewsPortalIds is null ? "" : string.Join(",", NewsPortalIds))}, " +
            $"{nameof(CategoryIds)}:{(CategoryIds is null ? "" : string.Join(",", CategoryIds))}, " +
            $"{nameof(TitleSearchQuery)}:{TitleSearchQuery}";
        }
        #endregion
    }
}
