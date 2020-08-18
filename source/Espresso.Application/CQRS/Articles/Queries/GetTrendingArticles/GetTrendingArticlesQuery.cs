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

        public TimeSpan MaxAgeOfTrendingArticle { get; }
        #endregion

        #region Constructors
        public GetTrendingArticlesQuery(
            int take,
            int skip,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType,
            TimeSpan maxAgeOfTrendingArticle
        ) : base(
            currentEspressoWebApiVersion: currentEspressoWebApiVersion,
            targetedEspressoWebApiVersion: targetedEspressoWebApiVersion,
            consumerVersion: consumerVersion,
            deviceType: deviceType,
            Event.GetTrendingArticlesQuery
        )
        {
            Take = take;
            Skip = skip;
            MaxAgeOfTrendingArticle = maxAgeOfTrendingArticle;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, " +
                $"{nameof(Skip)}:{Skip}, " +
                $"{nameof(MaxAgeOfTrendingArticle)}:{MaxAgeOfTrendingArticle}";
        }
        #endregion
    }
}
