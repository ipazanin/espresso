using Espresso.Application.Infrastructure;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles
{
    public class GetTrendingArticlesQuery : Request<GetTrendingArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; private set; }
        public int Skip { get; private set; }
        #endregion

        #region Constructors
        public GetTrendingArticlesQuery(
            int take,
            int skip,
            string currentEspressoWebApiVersion,
            string targetedEspressoWebApiVersion,
            string consumerVersion,
            DeviceType deviceType
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
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, {nameof(Skip)}:{Skip}";
        }
        #endregion
    }
}
