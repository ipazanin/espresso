using System;
using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQuery : Request<GetTrendingArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; }
        public int Skip { get; }
        public long? MinTimestamp { get; }

        public TimeSpan MaxAgeOfTrendingArticle { get; }
        #endregion

        #region Constructors
        public GetTrendingArticlesQuery(
            int take,
            int skip,
            long? minTimestamp,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            AppEnvironment appEnvironment,
            TimeSpan maxAgeOfTrendingArticle
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            appEnvironment: appEnvironment,
            Event.GetTrendingArticlesQuery
        )
        {
            Take = take;
            Skip = skip;
            MinTimestamp = minTimestamp;
            MaxAgeOfTrendingArticle = maxAgeOfTrendingArticle;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, " +
                $"{nameof(Skip)}:{Skip}, " +
                $"{nameof(MinTimestamp)}:{MinTimestamp}, " +
                $"{nameof(MaxAgeOfTrendingArticle)}:{MaxAgeOfTrendingArticle}";
        }
        #endregion
    }
}
