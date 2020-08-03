using Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes
{
    public class GetTrendingArticlesQueryResponseType :
        ObjectGraphType<GetTrendingArticlesQueryResponse>
    {
        public GetTrendingArticlesQueryResponseType()
        {
            Name = nameof(GetTrendingArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetTrendingArticlesArticleType>>>>(
                name: nameof(GetTrendingArticlesQueryResponse.Articles)
            );
        }
    }
}
