using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    public class GetLatestArticlesCategoryType : ObjectGraphType<GetLatestArticlesCategory>
    {
        public GetLatestArticlesCategoryType()
        {
            Name = nameof(GetLatestArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetLatestArticlesCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesCategory.Color)
            );
        }
    }
}
