using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes
{
    public class GetCategoryArticlesQueryResponseType :
        ObjectGraphType<GetCategoryArticlesQueryResponse>
    {
        public GetCategoryArticlesQueryResponseType()
        {
            Name = nameof(GetCategoryArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<ArticleViewModelType>>>>(
                name: nameof(GetCategoryArticlesQueryResponse.Articles)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetCategoryArticlesQueryResponse.NewNewsPortalsPosition)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<NewsPortalViewModelType>>>>(
                name: nameof(GetCategoryArticlesQueryResponse.NewNewsPortals)
            );
        }
    }
}
