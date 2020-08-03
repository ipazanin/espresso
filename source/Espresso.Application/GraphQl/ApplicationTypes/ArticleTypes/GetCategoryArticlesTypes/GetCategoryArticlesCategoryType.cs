using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes
{
    public class GetCategoryArticlesCategoryType : ObjectGraphType<GetCategoryArticlesCategory>
    {
        public GetCategoryArticlesCategoryType()
        {
            Name = nameof(GetCategoryArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Color)
            );
        }
    }
}
