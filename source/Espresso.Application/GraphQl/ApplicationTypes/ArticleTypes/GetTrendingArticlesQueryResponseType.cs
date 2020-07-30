using Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes
{
    public class GetTrendingArticlesQueryResponseType :
        ObjectGraphType<GetTrendingArticlesQueryResponse>
    {
        public GetTrendingArticlesQueryResponseType()
        {
            Name = nameof(GetTrendingArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<ArticleTrendingViewModelType>>>>(
                name: nameof(GetTrendingArticlesQueryResponse.Articles)
            );
        }
    }
}
