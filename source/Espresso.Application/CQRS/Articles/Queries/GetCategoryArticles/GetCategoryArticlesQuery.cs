using System.Collections.Generic;
using System.Linq;
using Espresso.Application.Infrastructure;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles
{
    public class GetCategoryArticlesQuery : Request<GetCategoryArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; }

        public int Skip { get; }

        public int CategoryId { get; }

        public IEnumerable<int>? NewsPortalIds { get; }
        #endregion

        #region Constructors
        public GetCategoryArticlesQuery(
            int? take,
            int? skip,
            int categoryId,
            string? newsPortalIdsString,
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.GetCategoryArticlesQuery
        )
        {
            Take = take ?? DefaultValueConstants.DefaultTake;
            Skip = skip ?? DefaultValueConstants.DefaultSkip;
            CategoryId = categoryId;

            var newsPortalIds = newsPortalIdsString
                ?.Replace(" ", "")
                ?.Split(',')
                ?.Select(newsPortalIdString => int.TryParse(newsPortalIdString, out var newsPortalId) ? newsPortalId : default)
                ?.Where(newsPortalId => newsPortalId != default);

            NewsPortalIds = newsPortalIds == null || newsPortalIds.Any() ? newsPortalIds : null;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Take)}:{Take}, {nameof(Skip)}:{Skip}, {nameof(CategoryId)}:{CategoryId}, " +
                $"{nameof(NewsPortalIds)}:{(NewsPortalIds is null ? "" : string.Join(",", NewsPortalIds))}";
        }
        #endregion
    }
}
