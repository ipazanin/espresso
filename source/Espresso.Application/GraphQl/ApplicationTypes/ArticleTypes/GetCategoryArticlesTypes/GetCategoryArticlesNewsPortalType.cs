using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes
{
    public class GetCategoryArticlesNewsPortalType : ObjectGraphType<GetCategoryArticlesNewsPortal>
    {
        public GetCategoryArticlesNewsPortalType()
        {
            Name = nameof(GetCategoryArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetCategoryArticlesNewsPortal.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesNewsPortal.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesNewsPortal.IconUrl)
            );
        }
    }
}
