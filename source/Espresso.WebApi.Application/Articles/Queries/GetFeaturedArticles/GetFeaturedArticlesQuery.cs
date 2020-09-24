﻿using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles
{
    public class GetFeaturedArticlesQuery : Request<GetFeaturedArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; }

        public int Skip { get; }
        public Guid? FirstArticleId { get; }
        public IEnumerable<int>? NewsPortalIds { get; }

        public IEnumerable<int>? CategoryIds { get; }

        public TimeSpan MaxAgeOfFeaturedArticle { get; }

        public TimeSpan MaxAgeOfTrendingArticle { get; }
        #endregion

        #region Constructors
        public GetFeaturedArticlesQuery(
            int take,
            int skip,
            Guid? firstArticleId,
            string? categoryIdsString,
            string? newsPortalIdsString,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment,
            TimeSpan maxAgeOfFeaturedArticle,
            TimeSpan maxAgeOfTrendingArticle
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            eventIdEnum: Event.GetFeaturedArticles
        )
        {
            Take = take;
            Skip = skip;
            FirstArticleId = firstArticleId;
            var newsPortalIds = newsPortalIdsString
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(
                    newsPortalIdString =>
                        int.TryParse(newsPortalIdString, out var newsPortalId) ?
                        newsPortalId :
                        default
                )
                ?.Where(newsPortalId => newsPortalId != default);

            NewsPortalIds = newsPortalIds is null || newsPortalIds.Any() ? newsPortalIds : null;

            var categoryIds = categoryIdsString
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(
                    categoryIdString =>
                        int.TryParse(categoryIdString, out var newsPortalId) ?
                        newsPortalId :
                        default
                )
                ?.Where(categoryId => categoryId != default);

            CategoryIds = categoryIds == null || categoryIds.Any() ? categoryIds : null;
            MaxAgeOfFeaturedArticle = maxAgeOfFeaturedArticle;
            MaxAgeOfTrendingArticle = maxAgeOfTrendingArticle;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, " +
            $"{nameof(Skip)}:{Skip}, " +
            $"{nameof(FirstArticleId)}:{FirstArticleId}, " +
            $"{nameof(NewsPortalIds)}:{(NewsPortalIds is null ? "" : string.Join(",", NewsPortalIds))}, " +
            $"{nameof(CategoryIds)}:{(CategoryIds is null ? "" : string.Join(",", CategoryIds))}";
        }
        #endregion
    }
}