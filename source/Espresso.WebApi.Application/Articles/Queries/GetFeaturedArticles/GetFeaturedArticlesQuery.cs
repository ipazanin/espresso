using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public record GetFeaturedArticlesQuery : Request<GetFeaturedArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; init; }

        public int Skip { get; init; }
        public Guid? FirstArticleId { get; init; }
        public string? NewsPortalIds { get; init; } = "";
        public string? CategoryIds { get; init; } = "";

        public TimeSpan MaxAgeOfFeaturedArticle { get; init; }

        public TimeSpan MaxAgeOfTrendingArticle { get; init; }
        #endregion

        #region Constructors
        // public GetFeaturedArticlesQuery(
        //     int take,
        //     int skip,
        //     Guid? firstArticleId,
        //     string? categoryIdsString,
        //     string? newsPortalIdsString,
        //     string currentEspressoWebApiVersion,
        //     string targetedEspressoWebApiVersion,
        //     string consumerVersion,
        //     DeviceType deviceType,
        //     AppEnvironment appEnvironment,
        //     TimeSpan maxAgeOfFeaturedArticle,
        //     TimeSpan maxAgeOfTrendingArticle
        // ) : base(
        //     currentEspressoWebApiVersion: currentEspressoWebApiVersion,
        //     targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
        //     consumerVersion: consumerVersion,
        //     deviceType: deviceType,
        //     appEnvironment: appEnvironment,
        //     eventIdEnum: Event.GetFeaturedArticles
        // )
        // {
        //     Take = take;
        //     Skip = skip;
        //     FirstArticleId = firstArticleId;
        //     var newsPortalIds = newsPortalIdsString
        //         ?.Replace(" ", "")
        //         ?.Split(',')
        //         ?.Select(
        //             newsPortalIdString =>
        //                 int.TryParse(newsPortalIdString, out var newsPortalId) ?
        //                 newsPortalId :
        //                 default
        //         )
        //         ?.Where(newsPortalId => newsPortalId != default);

        //     NewsPortalIds = newsPortalIds is null || newsPortalIds.Any() ? newsPortalIds : null;

        //     var categoryIds = categoryIdsString
        //         ?.Replace(" ", "")
        //         ?.Split(',')
        //         ?.Select(
        //             categoryIdString =>
        //                 int.TryParse(categoryIdString, out var newsPortalId) ?
        //                 newsPortalId :
        //                 default
        //         )
        //         ?.Where(categoryId => categoryId != default);

        //     CategoryIds = categoryIds == null || categoryIds.Any() ? categoryIds : null;
        //     MaxAgeOfFeaturedArticle = maxAgeOfFeaturedArticle;
        //     MaxAgeOfTrendingArticle = maxAgeOfTrendingArticle;
        // }
        #endregion
    }
}
