using Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes
{
    public class GetTrendingArticlesCategoryType : ObjectGraphType<GetTrendingArticlesCategory>
    {
        public GetTrendingArticlesCategoryType()
        {
            Name = nameof(GetTrendingArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetTrendingArticlesCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesCategory.Color)
            );
        }
    }
}
