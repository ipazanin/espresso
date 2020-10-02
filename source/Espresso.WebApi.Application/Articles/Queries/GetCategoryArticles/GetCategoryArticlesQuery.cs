using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles
{
    public record GetCategoryArticlesQuery : Request<GetCategoryArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; init; }

        public int Skip { get; init; }

        public Guid? FirstArticleId { get; init; }

        public int CategoryId { get; init; }

        public int? RegionId { get; init; }

        public string? NewsPortalIds { get; init; }

        public int NewNewsPortalsPosition { get; init; }

        public string? TitleSearchQuery { get; init; }

        public TimeSpan MaxAgeOfNewNewsPortal { get; init; }
        #endregion

        #region Constructors
        // public GetCategoryArticlesQuery(
        //     int take,
        //     int skip,
        //     Guid? firstArticleId,
        //     int categoryId,
        //     string? newsPortalIdsString,
        //     int? regionId,
        //     TimeSpan maxAgeOfNewNewsPortal,
        //     string currentEspressoWebApiVersion,
        //     string targetedEspressoWebApiVersion,
        //     string consumerVersion,
        //     DeviceType deviceType,
        //     AppEnvironment appEnvironment,
        //     int newNewsPortalsPosition,
        //     string? titleSearchQuery
        // ) : base(
        //     currentEspressoWebApiVersion: currentEspressoWebApiVersion,
        //     targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
        //     consumerVersion: consumerVersion,
        //     deviceType: deviceType,
        //     appEnvironment: appEnvironment,
        //     Event.GetCategoryArticlesQuery
        // )
        // {
        //     Take = take;
        //     Skip = skip;
        //     FirstArticleId = firstArticleId;
        //     CategoryId = categoryId;
        //     RegionId = regionId;
        //     MaxAgeOfNewNewsPortal = maxAgeOfNewNewsPortal;
        //     var newsPortalIds = newsPortalIdsString
        //         ?.Replace(" ", "")
        //         ?.Split(',')
        //         ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
        //         ?.Where(newsPortalId => newsPortalId != default);

        //     NewsPortalIds = newsPortalIds == null || newsPortalIds.Any() ? newsPortalIds : null;
        //     NewNewsPortalsPosition = newNewsPortalsPosition;
        //     TitleSearchQuery = titleSearchQuery;
        // }
        #endregion
    }
}
