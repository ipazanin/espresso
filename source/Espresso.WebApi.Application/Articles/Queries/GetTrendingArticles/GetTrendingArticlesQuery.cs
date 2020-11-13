using System;
using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles
{
    public record GetTrendingArticlesQuery : Request<GetTrendingArticlesQueryResponse>
    {
        #region Properties
        public int Take { get; init; }
        public int Skip { get; init; }
        public Guid? FirstArticleId { get; init; }
        public TimeSpan MaxAgeOfTrendingArticle { get; init; }
        #endregion
    }
}
