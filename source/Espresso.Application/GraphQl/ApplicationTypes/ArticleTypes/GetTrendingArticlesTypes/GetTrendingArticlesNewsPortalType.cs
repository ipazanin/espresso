using Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes
{
    public class GetTrendingArticlesNewsPortalType : ObjectGraphType<GetTrendingArticlesNewsPortal>
    {
        public GetTrendingArticlesNewsPortalType()
        {
            Name = nameof(GetTrendingArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetTrendingArticlesNewsPortal.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesNewsPortal.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesNewsPortal.IconUrl)
            );
        }
    }
}
