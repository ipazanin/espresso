using System;
using Espresso.WebApi.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.Application.CQRS.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQuery : Request<GetTrendingArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; }
        public int Skip { get; }
        public Guid? FirstArticleId { get; }

        public TimeSpan MaxAgeOfTrendingArticle { get; }
        #endregion

        #region Constructors
        public GetTrendingArticlesQuery(
            int take,
            int skip,
            Guid? firstArticleId,
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
            FirstArticleId = firstArticleId;
            MaxAgeOfTrendingArticle = maxAgeOfTrendingArticle;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, " +
                $"{nameof(Skip)}:{Skip}, " +
                $"{nameof(FirstArticleId)}:{FirstArticleId}, " +
                $"{nameof(MaxAgeOfTrendingArticle)}:{MaxAgeOfTrendingArticle}";
        }
        #endregion
    }
}
