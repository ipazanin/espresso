using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTrendingArticlesCategoryType : ObjectGraphType<GetTrendingArticlesCategory>
    {
        /// <summary>
        /// 
        /// </summary>
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
