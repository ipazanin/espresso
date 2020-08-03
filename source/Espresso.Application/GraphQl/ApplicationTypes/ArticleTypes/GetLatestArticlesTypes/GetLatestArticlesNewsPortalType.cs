using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    public class GetLatestArticlesNewsPortalType : ObjectGraphType<GetLatestArticlesNewsPortal>
    {
        public GetLatestArticlesNewsPortalType()
        {
            Name = nameof(GetLatestArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.IconUrl)
            );
        }
    }
}
