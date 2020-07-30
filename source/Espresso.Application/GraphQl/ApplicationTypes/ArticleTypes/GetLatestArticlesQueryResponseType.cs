using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes
{
    public class GetLatestArticlesQueryResponseType :
        ObjectGraphType<GetLatestArticlesQueryResponse>
    {
        public GetLatestArticlesQueryResponseType()
        {
            Name = nameof(GetLatestArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<ArticleViewModelType>>>>(
                name: nameof(GetLatestArticlesQueryResponse.Articles)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetLatestArticlesQueryResponse.NewNewsPortalsPosition)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<NewsPortalViewModelType>>>>(
                name: nameof(GetLatestArticlesQueryResponse.NewNewsPortals)
            );
        }
    }
}
