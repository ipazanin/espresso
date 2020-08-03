using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    public class GetLatestArticlesQueryResponseType :
        ObjectGraphType<GetLatestArticlesQueryResponse>
    {
        public GetLatestArticlesQueryResponseType()
        {
            Name = nameof(GetLatestArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetLatestArticlesArticleType>>>>(
                name: nameof(GetLatestArticlesQueryResponse.Articles)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetLatestArticlesQueryResponse.NewNewsPortalsPosition)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetLatestArticlesNewsPortalType>>>>(
                name: nameof(GetLatestArticlesQueryResponse.NewNewsPortals)
            );
        }
    }
}
